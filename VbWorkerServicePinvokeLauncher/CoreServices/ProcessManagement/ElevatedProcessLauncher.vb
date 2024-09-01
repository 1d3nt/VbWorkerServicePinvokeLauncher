Imports VbWorkerServicePinvokeLauncher.CoreServices.NativeMethods.Methods
Imports VbWorkerServicePinvokeLauncher.Utilities

Namespace CoreServices.ProcessManagement

    ''' <summary>
    ''' The <c>ElevatedProcessLauncher</c> class provides functionality to run processes under elevated privileges, 
    ''' such as the SYSTEM account. It simplifies the process of launching an executable with duplicated 
    ''' access tokens, allowing the new process to inherit the security context of a specified process.
    ''' </summary>
    ''' <remarks>
    ''' This class is particularly useful in scenarios where it is necessary to execute a process with 
    ''' higher privileges, such as running tasks that require administrative or SYSTEM-level access.
    ''' The class works by duplicating the access token of an existing process (identified by its name) 
    ''' and using this token to create a new process with the same elevated permissions.
    ''' 
    ''' The main steps involved in this class are:
    ''' <list type="number">
    '''     <item>
    '''         <description>Retrieving the active console session ID to identify the session associated with the interactive user.</description>
    '''     </item>
    '''     <item>
    '''         <description>Opening the process specified by its name to obtain a handle that allows token duplication.</description>
    '''     </item>
    '''     <item>
    '''         <description>Duplicating the security token associated with the specified process to create a new token.</description>
    '''     </item>
    '''     <item>
    '''         <description>Using the duplicated token to create and launch a new process that inherits the security context of the original process.</description>
    '''     </item>
    ''' </list>
    ''' 
    ''' This mechanism is often used in administrative tasks where processes need to run with SYSTEM privileges, 
    ''' such as background services or maintenance scripts. By using the SYSTEM account or a similar high-privilege 
    ''' account, the launched process can perform tasks that are normally restricted for standard user accounts.
    ''' </remarks>
    Friend Class ElevatedProcessLauncher

        ''' <summary>
        ''' The default instance under which to run the process. 
        ''' This constant represents the default account or instance used to run the duplicated process.
        ''' </summary>
        ''' <remarks>
        ''' The value of this constant, "winlogon", specifies the system process that is used as the context under which the new process will run. 
        ''' This is typically used to ensure that the process runs with elevated privileges and under the security context of the system, 
        ''' which can be important for tasks requiring high-level permissions.
        ''' </remarks>
        Private Const DefaultRunAs As String = "winlogon"

        ''' <summary>
        ''' Represents the name of the default interactive window station and desktop.
        ''' </summary>
        ''' <remarks>
        ''' This constant is used to specify the interactive window station and desktop, 
        ''' typically referred to as "winsta0\default." The interactive window station 
        ''' allows processes to interact with the user interface, making it possible for 
        ''' processes running under the SYSTEM account or other elevated privileges to display 
        ''' windows and interact with the desktop. 
        ''' 
        ''' In the context of launching elevated processes, setting the window station 
        ''' and desktop to "winsta0\default" ensures that the process has the necessary 
        ''' interface access to interact with the logged-on user's desktop environment.
        ''' </remarks>
        Private Const WindowStationDesktopCreation As String = "winsta0\default"

        ''' <summary>
        ''' Manages security tokens, including duplication and retrieval.
        ''' </summary>
        Private ReadOnly _processTokenManager As ProcessTokenManager

        ''' <summary>
        ''' Retrieves process and session information.
        ''' </summary>
        Private ReadOnly _processInfoRetriever As ProcessInfoRetriever

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ElevatedProcessLauncher"/> class.
        ''' </summary>
        ''' <param name="tokenManager">The token manager responsible for handling security tokens.</param>
        ''' <param name="processInfoRetriever">The process information retriever responsible for retrieving process and session information.</param>
        Friend Sub New(tokenManager As ProcessTokenManager, processInfoRetriever As ProcessInfoRetriever)
            _processTokenManager = tokenManager
            _processInfoRetriever = processInfoRetriever
        End Sub

        ''' <summary>
        ''' Creates and executes a new process with elevated privileges, impersonating a specified process.
        ''' </summary>
        ''' <param name="applicationPath">
        ''' The path to the executable that this method will manage.
        ''' </param>
        ''' <remarks>
        ''' This method is responsible for launching a new process under an elevated context, using the 
        ''' security token of an existing process identified by the <c>DefaultRunAs</c> parameter. 
        ''' It involves duplicating the process token of the specified process, creating a new process with 
        ''' the necessary privileges, and then cleaning up handles after the process is launched.
        ''' 
        ''' The method follows these steps:
        ''' 1. Retrieves the session ID of the active console session.
        ''' 2. Finds the process ID of the specified process running in that session.
        ''' 3. Opens a handle to the process.
        ''' 4. Opens and duplicates the process token to create a new token with elevated privileges.
        ''' 5. Creates the new process using the duplicated token.
        ''' 6. Closes all opened handles after the process is launched.
        ''' 
        ''' This method is marked with a suppression attribute because ReSharper may incorrectly suggest 
        ''' that the method should be marked as <c>Shared</c> (static). However, the method is not <c>Shared</c> 
        ''' because it relies on instance-level operations and creating new instances of the class.
        ''' </remarks>
        Friend Sub TryCreateProcess(applicationPath As String)
            Const dwCreationFlags = AccessMask.NormalPriorityClass Or AccessMask.CreateNewConsole
            Dim dwSessionId = Methods.NativeMethods.WTSGetActiveConsoleSessionId()
            Dim specifiedId = _processInfoRetriever.GetSpecifiedId(DefaultRunAs, dwSessionId)
            Dim processHandle As IntPtr = TryOpenProcess(specifiedId)
            If Equals(processHandle, IntPtr.Zero) Then
                HandleManager.CloseTokenHandleIfNotNull(processHandle)
                Exit Sub
            End If
            Dim application = PathFormatter.CreateRelativePath(applicationPath)
            Dim tokenHandle As IntPtr = IntPtr.Zero
            Dim openProcessToken As Boolean = _processTokenManager.TryOpenProcessToken(processHandle, tokenHandle)
            If Not openProcessToken Then
                HandleManager.CloseTokenHandleIfNotNull(processHandle)
                Exit Sub
            End If
            Dim hToken As IntPtr = IntPtr.Zero
            Dim attributes As SecurityAttributes = _processTokenManager.GetSecurityAttributes()
            Dim duplicateToken As Boolean = _processTokenManager.TryDuplicateToken(attributes, tokenHandle, hToken)
            If Not duplicateToken Then
                HandleManager.CloseTokenHandleIfNotNull(processHandle)
                HandleManager.CloseTokenHandleIfNotNull(tokenHandle)
                Exit Sub
            End If
            Dim startupInfo As StartupInfoA = GetStartupInfo()
            Dim processInformation As New ProcessInformation()
            TryCreateProcessAsUser(hToken, application, attributes, dwCreationFlags, startupInfo, processInformation)
            HandleManager.CloseTokenHandleIfNotNull(processHandle)
            HandleManager.CloseTokenHandleIfNotNull(tokenHandle)
            HandleManager.CloseTokenHandleIfNotNull(hToken)
        End Sub

        ''' <summary>
        ''' Attempts to open a handle to a process using its specified process identifier.
        ''' This method wraps the <c>OpenProcess</c> Win32 API function, allowing the caller to obtain 
        ''' a handle to the process specified by its ID.
        ''' </summary>
        ''' <param name="specifiedId">
        ''' The identifier of the process to open.
        ''' </param>
        ''' <returns>
        ''' If the function succeeds, the return value is a valid handle to the specified process. 
        ''' If the function fails, the return value is <c>IntPtr.Zero</c>.
        ''' </returns>
        ''' <remarks>
        ''' This method calls the <see cref="NativeMethods.OpenProcess"/> function with the access 
        ''' mask <see cref="AccessMask.QueryInformation"/> to retrieve a handle to the specified process. 
        ''' For more information on the <c>OpenProcess</c> function, see the 
        ''' <see href="https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-openprocess">
        ''' official documentation</see>.
        ''' </remarks>
        Private Shared Function TryOpenProcess(specifiedId As UInteger) As IntPtr
            Return Methods.NativeMethods.OpenProcess(AccessMask.QueryInformation, False, specifiedId)
        End Function

        ''' <summary>
        ''' Creates a new process and its primary thread. The new process runs in the security context of the user represented
        ''' by the specified token.
        ''' </summary>
        ''' <param name="hToken">
        ''' A handle to the duplicate access token associated with the process. This token is obtained from the <see cref="ProcessTokenManager.TryDuplicateToken"/>
        ''' method and allows the new process to run in the security context of the user represented by the token.
        ''' </param>
        ''' <param name="application">
        ''' The full path to the executable file for the new process, including any command line parameters. This path is used to specify
        ''' the application to be launched.
        ''' </param>
        ''' <param name="attributes">
        ''' A <see cref="SecurityAttributes"/> structure that specifies a security descriptor for the new process object and determines
        ''' whether child processes can inherit the returned handle to the process. This structure is obtained from the <see cref="ProcessTokenManager.GetSecurityAttributes"/>
        ''' method.
        ''' </param>
        ''' <param name="dwCreationFlags">
        ''' The flags that control the priority class and the creation of the process. This parameter can include flags such as
        ''' <see cref="AccessMask.NormalPriorityClass"/> and <see cref="AccessMask.CreateNewConsole"/>. For a list of valid flags, see
        ''' <see href="https://learn.microsoft.com/en-us/windows/win32/procthread/process-creation-flags">Process Creation Flags</see>.
        ''' </param>
        ''' <param name="startupInfo">
        ''' A <see cref="StartupInfoA"/> structure that specifies the window station, desktop, standard handles, and appearance of the
        ''' main window for the process. This is obtained from the <see cref="GetStartupInfo"/> method.
        ''' </param>
        ''' <param name="processInformation">
        ''' A <see cref="ProcessInformation"/> structure that receives identification information about the new process, such as its handle
        ''' and process ID. This structure is populated by the method and should be checked to obtain details about the newly created process.
        ''' </param>
        ''' <remarks>
        ''' This method internally calls the <see cref="NativeMethods.CreateProcessAsUser"/> function to create the new process.
        ''' For more information on the native API function, refer to the <see href="https://docs.microsoft.com/en-us/windows/desktop/api/securitybaseapi/nf-securitybaseapi-createprocessasusera">CreateProcessAsUserA</see> documentation.
        ''' </remarks>
        Private Shared Sub TryCreateProcessAsUser(hToken As IntPtr, application As String, ByRef attributes As SecurityAttributes, dwCreationFlags As AccessMask, ByRef startupInfo As StartupInfoA, ByRef processInformation As ProcessInformation)
            Methods.NativeMethods.CreateProcessAsUser(hToken, Nothing, application, attributes, attributes, True, dwCreationFlags, IntPtr.Zero, Nothing, startupInfo, processInformation)
        End Sub

        ''' <summary>
        ''' Creates and initializes a <see cref="StartupInfoA"/> structure that contains the startup information for a process at creation time.
        ''' </summary>
        ''' <returns>
        ''' A <see cref="StartupInfoA"/> structure that specifies the window station, desktop, standard handles, and appearance of the main window for the process. 
        ''' The structure is configured with the default window station and desktop, and includes the <see cref="AccessMask.StartFUseStdHandles"/> flag for standard handles.
        ''' </returns>
        ''' <remarks>
        ''' The <see cref="StartupInfoA"/> structure is used by the <see cref="NativeMethods.CreateProcessAsUser"/> function to determine the initial state and configuration of the process.
        ''' For more details on the structure and its fields, refer to the <see href="https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/ns-processthreadsapi-startupinfoa">StartupInfoA documentation</see>.
        ''' </remarks>
        Private Shared Function GetStartupInfo() As StartupInfoA
            Dim startupInfo As New StartupInfoA() With {
                        .cb = Marshal.SizeOf(startupInfo),
                        .lpDesktop = WindowStationDesktopCreation,
                        .dwFlags = startupInfo.dwFlags Or AccessMask.StartFUseStdHandles
                    }
            Return startupInfo
        End Function
    End Class
End Namespace

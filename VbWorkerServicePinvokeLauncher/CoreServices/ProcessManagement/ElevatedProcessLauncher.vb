Namespace CoreServices.ProcessManagement

    ''' <summary>
    '''     Simplifies Running the remote process in the System account.
    ''' </summary>
    Public Class ElevatedProcessLauncher

        ''' <summary>
        '''     The value of the interactive window station.
        ''' </summary>
        Private Const MWindowStationDesktopCreation As String = "winsta0\default"

        ''' <summary>
        '''     Responsible for creating and executing a new process.
        ''' </summary>
        ''' <param name="applicationPath">
        '''     The path to the executable that this class will manage.
        ''' </param>
        ''' <param name="processName">
        '''     The process to Impersonate process name.
        ''' </param>
        ''' <remarks>
        '''     The <see cref="ElevatedProcessLauncher" /> class is responsible for managing a process that will run with elevated
        '''     privileges.
        ''' </remarks>
        <SuppressMessage("StaticMembers", "CA1822:Mark members as static", Justification:="Resharper incorrectly suggests marking these methods as Shared, even though they instantiate new objects of the class. Making them Shared would prevent instance creation and break the intended functionality.")>
        Friend Sub TryCreateProcess(applicationPath As String, processName As String)
            Const dwCreationFlags = AccessMask.NormalPriorityClass Or AccessMask.CreateNewConsole
            Dim dwSessionId = Methods.NativeMethods.WTSGetActiveConsoleSessionId()
            Dim specifiedId = GetSpecifiedId(processName, dwSessionId)
            Dim processHandle As IntPtr = TryOpenProcess(specifiedId)
            If Equals(processHandle, IntPtr.Zero) Then 
                Methods.NativeMethods.CloseHandle(processHandle)
                Exit Sub
            End If
            Dim application = $"{CreateRelativePath(applicationPath)}"
            Dim tokenHandle As IntPtr = IntPtr.Zero
            Dim openProcessToken As Boolean = ProcessTokenManager.TryOpenProcessToken(processHandle, tokenHandle)
            If Not openProcessToken Then
                Methods.NativeMethods.CloseHandle(processHandle)
                Exit Sub
            End If
            Dim hToken As IntPtr = IntPtr.Zero
            Dim attributes As SecurityAttributes = GetSecurityAttributes()
            Dim duplicateToken As Boolean = ProcessTokenManager.TryDuplicateToken(attributes, tokenHandle, hToken)
            If Not duplicateToken Then
                Methods.NativeMethods.CloseHandle(processHandle)
                Methods.NativeMethods.CloseHandle(tokenHandle)
                Exit Sub
            End If
            Dim startupInfo As StartupInfoA = GetStartupInfo()
            Dim processInformation As New ProcessInformation()
            TryCreateProcessAsUser(dwCreationFlags, startupInfo, processInformation, application, attributes, hToken)
            Methods.NativeMethods.CloseHandle(processHandle)
            Methods.NativeMethods.CloseHandle(tokenHandle)
            Methods.NativeMethods.CloseHandle(hToken)
        End Sub

        ''' <summary>
        '''     Using the calling process we can then call the OpenProcess Win32 API to obtain a handle from the specified ID.
        ''' </summary>
        ''' <param name="specifiedId">
        '''     The specified process identifier of the process to open.
        ''' </param>
        ''' <returns>
        '''     If the function succeeds, the return value is an open handle to the specified process. If the function fails, the
        '''     return value is NULL.
        ''' </returns>
        Private Shared Function TryOpenProcess(specifiedId As UInteger) As IntPtr
            Return Methods.NativeMethods.OpenProcess(AccessMask.QueryInformation, False, specifiedId)
        End Function

        ''' <summary>
        '''     Creates a new process and its primary thread. The new process runs in the security context of the user represented
        '''     by the specified token.
        ''' </summary>
        ''' <param name="dwCreationFlags">
        '''     The access flags to control the ability of the process.
        ''' </param>
        ''' <param name="startupInfo">
        '''     The startup attributes returned from the <see cref="GetStartupInfo" /> method.
        ''' </param>
        ''' <param name="processInformation">
        '''     The <see cref="ProcessInformation" /> that contains the process startup information.
        ''' </param>
        ''' <param name="application">
        '''     The application to create including any command line parameters.
        ''' </param>
        ''' <param name="attributes">
        '''     The security attributes returned from the <see cref="SecurityAttributes" /> method.
        ''' </param>
        ''' <param name="hToken">
        '''     The duplicate access token associated with the process returned from <see cref="ProcessTokenManager.TryDuplicateToken" /> method.
        ''' </param>
        Private Shared Sub TryCreateProcessAsUser(dwCreationFlags As AccessMask, ByRef startupInfo As StartupInfoA, ByRef processInformation As ProcessInformation, application As String, ByRef attributes As SecurityAttributes, hToken As IntPtr)
            Methods.NativeMethods.CreateProcessAsUser(hToken, Nothing, application, attributes, attributes, True, dwCreationFlags, IntPtr.Zero, Nothing, startupInfo, processInformation)
        End Sub

        ''' <summary>
        '''     A structure and associated data that contains the startup information for a process at creation time.
        ''' </summary>
        ''' <returns>
        '''     The <see cref="StartupInfoA" /> that contains the startup attributes.
        ''' </returns>
        Private Shared Function GetStartupInfo() As StartupInfoA
            Dim startupInfo As New StartupInfoA() With {.cb = Marshal.SizeOf(startupInfo), .lpDesktop = MWindowStationDesktopCreation, .dwFlags = startupInfo.dwFlags Or AccessMask.StartFUseStdHandles}
            Return startupInfo
        End Function

        ''' <summary>
        '''     A structure and associated data that contains the security information for a securable object.
        ''' </summary>
        ''' <returns>
        '''     The <see cref="SecurityAttributes" /> that contains the security attributes.
        ''' </returns>
        Private Shared Function GetSecurityAttributes() As SecurityAttributes
            Dim attributes As New SecurityAttributes() With {.nLength = Marshal.SizeOf(attributes), .bInheritHandle = True}
            Return attributes
        End Function

        ''' <summary>
        '''     Gets the process ID where the console's session identifier matches the Terminal Services session identifier.
        ''' </summary>
        ''' <param name="processName">
        '''     The name that the system uses to identify the process to the user.
        ''' </param>
        ''' <param name="dwSessionId">
        '''     The session identifier of the console session.
        ''' </param>
        ''' <returns>
        '''     The unique identifier for the associated process as a <c>UInteger</c>.
        ''' </returns>
        Private Shared Function GetSpecifiedId(processName As String, dwSessionId As UInteger) As UInteger
            Return CUInt((From p In Process.GetProcessesByName(processName) Where p.SessionId = dwSessionId Select p.Id).FirstOrDefault())
        End Function

        ''' <summary>
        '''     Formats forward slashes in favor of backslashes to avoid command line option specifier conflicts(DOS).
        ''' </summary>
        ''' <param name="value">
        '''     The path to evaluate.
        ''' </param>
        ''' <returns>
        '''     A correctly formatted path.
        ''' </returns>
        Private Shared Function CreateRelativePath(value As String) As String
            Return Text.RegularExpressions.Regex.Replace(value, "/", "\")
        End Function
    End Class
End Namespace

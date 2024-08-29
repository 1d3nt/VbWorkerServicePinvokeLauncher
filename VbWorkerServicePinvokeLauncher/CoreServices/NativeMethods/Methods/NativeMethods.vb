Imports VbWorkerServicePinvokeLauncher.Classes.Processes.Structures
Imports VbWorkerServicePinvokeLauncher.Enums
Imports VbWorkerServicePinvokeLauncher.Structures

Namespace CoreServices.NativeMethods.Methods

Friend Class NativeMethods

    ''' <summary>
    '''     Retrieves information about the specified process.
    ''' </summary>
    ''' <param name="processHandle">
    '''     A handle to the process for which information is to be retrieved.
    ''' </param>
    ''' <param name="processInformationClass">
    '''     The type of process information to be retrieved. This parameter can be one of the following values from the 
    '''     PROCESSINFOCLASS enumeration.
    ''' </param>
    ''' <param name="processInformation">
    '''     A pointer to a buffer supplied by the calling application into which the function writes the requested information. 
    '''     The size of the information written varies depending on the data type of the ProcessInformationClass parameter:
    ''' </param>
    ''' <param name="processInformationLength">
    '''     The size of the buffer pointed to by the ProcessInformation parameter, in bytes.
    ''' </param>
    ''' <param name="returnLength">
    '''     A pointer to a variable in which the function returns the size of the requested information. If the function was successful, 
    '''     this is the size of the information written to the buffer pointed to by the ProcessInformation parameter, 
    '''     but if the buffer was too small, this is the minimum size of buffer needed to receive the information successfully.
    ''' </param>
    ''' <returns>
    '''     The function returns an NTSTATUS success or error code.
    '''
    '''     The forms and significance of NTSTATUS error codes are listed in the Ntstatus.h header file available in the DDK, and are 
    '''     described in the DDK documentation under Kernel-Mode Driver Architecture / Design Guide / Driver Programming Techniques / 
    '''     Logging Errors.
    ''' </returns>
    <DllImport(ExternDll.Ntdll)>
    Friend Shared Function NtQueryInformationProcess(processHandle As IntPtr,
                                                     processInformationClass As Integer,
                                                     ByRef processInformation As ProcessBasicInformation,
                                                     processInformationLength As Integer,
                                                     ByRef returnLength As Integer) As Integer
    End Function

    ''' <summary>
    '''     Retrieves the session identifier of the console session. The console session is the session that is currently
    '''     attached to the physical console.
    '''     Note that it is not necessary that Remote Desktop Services be running for this function to succeed.
    ''' </summary>
    ''' <returns>
    '''     The session identifier of the session that is attached to the physical console. If there is no session attached to
    '''     the physical console,
    '''     (for example, if the physical console session is in the process of being attached or detached), this function
    '''     returns 0xFFFFFFFF.
    ''' </returns>
    ''' <remarks>
    '''     See https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-wtsgetactiveconsolesessionid
    ''' </remarks>
    <DllImport(ExternDll.Kernel32)>
    Friend Shared Function WTSGetActiveConsoleSessionId() As Integer
    End Function

    ''' <summary>
    '''     Retrieves the process identifier of the calling process.
    ''' </summary>
    ''' <returns>
    '''     Until the process terminates, the process identifier uniquely identifies the process throughout the system.
    ''' </returns>
    <DllImport(ExternDll.Kernel32, SetLastError:=True)>
    Friend Shared Function GetCurrentProcessId() As Integer
    End Function

    ''' <summary>
    '''     Closes an open object handle.
    ''' </summary>
    ''' <param name="hObject">
    '''     A valid handle to an open object.
    ''' </param>
    ''' <returns>
    '''     If the function succeeds, the return value is nonzero.
    ''' </returns>
    ''' <remarks>
    '''     See https://msdn.microsoft.com/en-us/library/windows/desktop/ms724211(v=vs.85).aspx
    ''' </remarks>
    <DllImport(ExternDll.Kernel32, SetLastError:=True)>
    Friend Shared Function CloseHandle(<[In]> hObject As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    ''' <summary>
    '''     Opens an existing local process object.
    ''' </summary>
    ''' <param name="dwDesiredAccess">
    '''     The access to the process object. This access right is checked against the
    '''     security descriptor for the process. This parameter can be one or more of the process access rights.
    ''' </param>
    ''' <param name="bInheritHandle">
    '''     If this value is TRUE, processes created by this process will inherit the handle.
    '''     Otherwise, the processes do not inherit this handle.
    ''' </param>
    ''' <param name="dwProcessId">
    '''     The identifier of the local process to be opened.
    ''' </param>
    ''' <returns>
    '''     If the function succeeds, the return value is an open handle to the specified process.
    '''     If the function fails, the return value is NULL.
    ''' </returns>
    ''' <remarks>
    '''     See https://docs.microsoft.com/en-us/windows/desktop/api/processthreadsapi/nf-processthreadsapi-openprocess
    ''' </remarks>
    <DllImport(ExternDll.Kernel32, SetLastError:=True)>
    Friend Shared Function OpenProcess(<[In]> dwDesiredAccess As AccessRights,
                                       <[In]> bInheritHandle As Boolean,
                                       <[Out]> dwProcessId As Integer) As IntPtr
    End Function

    ''' <summary>
    '''     Creates a new primary token or impersonation token that duplicates an existing token.
    ''' </summary>
    ''' <param name="ProcessHandle">
    '''     A handle to the process whose access token is opened.
    '''     The process must have the PROCESS_QUERY_INFORMATION access permission.
    ''' </param>
    ''' <param name="DesiredAccess">
    '''     Specifies an access mask that specifies the requested types of access to the access token.
    '''     These requested access types are compared with the discretionary access control list (DACL) of the token to
    '''     determine which accesses are granted or denied.
    ''' </param>
    ''' <param name="TokenHandle">
    '''     A pointer to a handle that identifies the newly opened access token when the function returns.
    ''' </param>
    ''' <returns>
    '''     If the function succeeds, the return value is nonzero.
    ''' </returns>
    ''' <remarks>
    '''     See https://docs.microsoft.com/en-us/windows/desktop/api/processthreadsapi/nf-processthreadsapi-openprocesstoken
    ''' </remarks>
    <DllImport(ExternDll.Advapi32, SetLastError:=True)>
    Friend Shared Function OpenProcessToken(processHandle As IntPtr,
                                            desiredAccess As AccessRights,
                                            ByRef tokenHandle As IntPtr) As Boolean
    End Function

    ''' <summary>
    '''     The DuplicateTokenEx function creates a new access token that duplicates an existing token. This function
    '''     can create either a primary token or an impersonation token.
    ''' </summary>
    ''' <param name="hExistingToken">
    '''     A handle to an access token opened with TOKEN_DUPLICATE access.
    ''' </param>
    ''' <param name="dwDesiredAccess">
    '''     Specifies the requested access rights for the new token. The DuplicateTokenEx function compares the requested
    '''     access rights with the existing token's
    '''     discretionary access control list (DACL) to determine which rights are granted or denied. To request the same
    '''     access rights as the existing token,
    '''     specify zero. To request all access rights that are valid for the caller, specify MAXIMUM_ALLOWED.
    ''' </param>
    ''' <param name="lpTokenAttributes">
    '''     A pointer to a SECURITY_ATTRIBUTES structure that specifies a security descriptor for the new token and determines
    '''     whether child processes can inherit
    '''     the token. If lpTokenAttributes is NULL, the token gets a default security descriptor and the handle cannot be
    '''     inherited. If the security descriptor
    '''     contains a system access control list (SACL), the token gets ACCESS_SYSTEM_SECURITY access right, even if it was
    '''     not requested in dwDesiredAccess.
    ''' </param>
    ''' <param name="impersonationLevel">
    '''     Specifies a value from the SECURITY_IMPERSONATION_LEVEL enumeration that indicates the impersonation level of the
    '''     new token.
    ''' </param>
    ''' <param name="tokenType">
    '''     Specifies one of the following values from the TOKEN_TYPE enumeration.
    ''' </param>
    ''' <param name="phNewToken">
    '''     A pointer to a HANDLE variable that receives the new token.
    ''' </param>
    ''' <returns>
    '''     If the function succeeds, the function returns a nonzero value.
    ''' </returns>
    ''' <remarks>
    '''     See https://docs.microsoft.com/en-us/windows/desktop/api/securitybaseapi/nf-securitybaseapi-duplicatetokenex
    ''' </remarks>
    <DllImport(ExternDll.Advapi32)>
    Friend Shared Function DuplicateTokenEx(hExistingToken As IntPtr,
                                            dwDesiredAccess As UInteger,
                                            ByRef lpTokenAttributes As SecurityAttributes,
                                            impersonationLevel As Integer,
                                            tokenType As Integer,
                                            ByRef phNewToken As IntPtr) As Boolean
    End Function

    ''' <summary>
    '''     Creates a new process and its primary thread. The new process runs in the security context of the user represented
    '''     by the specified token.
    ''' </summary>
    ''' <param name="hToken">
    '''     A handle to the primary token that represents a user. The handle must have the TOKEN_QUERY, TOKEN_DUPLICATE, and
    '''     TOKEN_ASSIGN_PRIMARY access rights.
    '''     For more information, see Access Rights for Access-Token Objects. The user represented by the token must have read
    '''     and execute access to the application
    '''     specified by the lpApplicationName or the lpCommandLine parameter.
    ''' </param>
    ''' <param name="lpApplicationName">
    '''     The name of the module to be executed. This module can be a Windows-based application. It can be some other type of
    '''     module (for example, MS-DOS or OS/2)
    '''     if the appropriate subsystem is available on the local computer.
    ''' </param>
    ''' <param name="lpCommandLine">
    '''     The command line to be executed. The maximum length of this string is 32K characters. If lpApplicationName is NULL,
    '''     the module name portion of lpCommandLine is
    '''     limited to MAX_PATH characters.
    ''' </param>
    ''' <param name="lpProcessAttributes">
    '''     A pointer to a SECURITY_ATTRIBUTES structure that specifies a security descriptor for the new process object and
    '''     determines whether child processes can
    '''     inherit the returned handle to the process. If lpProcessAttributes is NULL or lpSecurityDescriptor is NULL, the
    '''     process gets a default security descriptor
    '''     and the handle cannot be inherited. The default security descriptor is that of the user referenced in the hToken
    '''     parameter. This security descriptor may not
    '''     allow access for the caller, in which case the process may not be opened again after it is run. The process handle
    '''     is valid and will continue
    '''     to have full access rights.
    ''' </param>
    ''' <param name="lpThreadAttributes">
    '''     A pointer to a SECURITY_ATTRIBUTES structure that specifies a security descriptor for the new thread object and
    '''     determines whether child processes can
    '''     inherit the returned handle to the thread. If lpThreadAttributes is NULL or lpSecurityDescriptor is NULL, the
    '''     thread gets a default security descriptor
    '''     and the handle cannot be inherited. The default security descriptor is that of the user referenced in the hToken
    '''     parameter. This security descriptor may
    '''     not allow access for the caller.
    ''' </param>
    ''' <param name="bInheritHandle">
    '''     If this parameter is TRUE, each inheritable handle in the calling process is inherited by the new process. If the
    '''     parameter is FALSE, the handles are
    '''     not inherited. Note that inherited handles have the same value and access rights as the original handles.
    ''' </param>
    ''' <param name="dwCreationFlags">
    '''     The flags that control the priority class and the creation of the process. For a list of values, see Process
    '''     Creation Flags.
    ''' </param>
    ''' <param name="lpEnvironment">
    '''     A pointer to an environment block for the new process. If this parameter is NULL, the new process uses the
    '''     environment of the calling process.
    ''' </param>
    ''' <param name="lpCurrentDirectory">
    '''     The full path to the current directory for the process. The string can also specify a UNC path.
    ''' </param>
    ''' <param name="lpStartupInfo">
    '''     A pointer to a STARTUPINFO or STARTUPINFOEX structure.
    ''' </param>
    ''' <param name="lpProcessInformation">
    '''     A pointer to a PROCESS_INFORMATION structure that receives identification information about the new process.
    ''' </param>
    ''' <returns>
    '''     If the function succeeds, the return value is nonzero.
    ''' </returns>
    <DllImport(ExternDll.Advapi32, SetLastError:=True)>
    Friend Shared Function CreateProcessAsUser(hToken As IntPtr, lpApplicationName As String,
                                               lpCommandLine As String,
                                               ByRef lpProcessAttributes As SecurityAttributes,
                                               ByRef lpThreadAttributes As SecurityAttributes,
                                               bInheritHandle As Boolean, dwCreationFlags As AccessRights,
                                               lpEnvironment As IntPtr, lpCurrentDirectory As String,
                                               ByRef lpStartupInfo As StartupInfoA,
                                               <Out> ByRef lpProcessInformation As ProcessInformation) As Boolean
    End Function
End Class

End Namespace

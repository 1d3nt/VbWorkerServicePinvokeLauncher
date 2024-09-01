Namespace CoreServices.NativeMethods.Methods

    ''' <summary>
    ''' Provides methods for interacting with native Windows APIs. 
    ''' This class contains P/Invoke declarations for various functions used for process and token management.
    ''' </summary>
    ''' <remarks>
    ''' The <see cref="NativeMethods"/> class uses the <c>DllImport</c> attribute to define methods that are imported 
    ''' from unmanaged DLLs. These methods are used to interact with the Windows operating system at a low level.
    ''' 
    ''' The <c>SuppressUnmanagedCodeSecurity</c> attribute is applied to this class to improve performance when
    ''' calling unmanaged code. This attribute disables code access security checks for unmanaged code, which
    ''' can reduce overhead in performance-critical applications. Use this attribute with caution, as it bypasses
    ''' some of the security measures provided by the .NET runtime.
    ''' </remarks>
    <Security.SuppressUnmanagedCodeSecurity>
    Friend NotInheritable Class NativeMethods

        ''' <summary>
        ''' Opens an existing local process object.
        ''' </summary>
        ''' <param name="dwDesiredAccess">
        ''' The access to the process object. This access right is checked against the security descriptor for the process. 
        ''' This parameter can be one or more of the process access rights. 
        ''' This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <param name="bInheritHandle">
        ''' If this value is <c>True</c>, processes created by this process will inherit the handle. Otherwise, the processes
        ''' do not inherit this handle. 
        ''' This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <param name="dwProcessId">
        ''' The identifier of the local process to be opened. 
        ''' This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <returns>
        ''' If the function succeeds, the return value is an open handle to the specified process. If the function fails, 
        ''' the return value is <c>IntPtr.Zero</c>.
        ''' </returns>
        ''' <remarks>
        ''' The function signature in C++ is:
        ''' <code>
        ''' HANDLE OpenProcess(
        '''   [in] DWORD dwDesiredAccess,
        '''   [in] BOOL bInheritHandle,
        '''   [in] DWORD dwProcessId
        ''' );
        ''' </code>
        ''' For more details, refer to:
        ''' <see href="https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-openprocess">OpenProcess</see>.
        ''' </remarks>
        <DllImport(ExternDll.Kernel32, SetLastError:=True)>
        Friend Shared Function OpenProcess(
            <[In]> dwDesiredAccess As UInteger,
            <[In]> bInheritHandle As Boolean,
            <[In]> dwProcessId As UInteger
        ) As IntPtr
        End Function

        ''' <summary>
        ''' Opens the access token associated with a process.
        ''' </summary>
        ''' <param name="processHandle">
        ''' A handle to the process whose access token is opened. The process handle must have appropriate access rights,
        ''' such as <see cref="AccessMask.TokenDuplicate"/> or <see cref="AccessMask.QueryInformation"/>, depending on 
        ''' the desired access to the token. This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <param name="desiredAccess">
        ''' Specifies an access mask that specifies the requested types of access to the access token. For example, to duplicate
        ''' the token, you might use <see cref="AccessMask.TokenDuplicate"/>. This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <param name="tokenHandle">
        ''' A pointer to a handle that receives the access token for the process when the function returns. This handle is used
        ''' to identify the token. This parameter is written to by the function and is passed with the <c>[Out]</c> attribute.
        ''' </param>
        ''' <returns>
        ''' If the function succeeds, the return value is <c>True</c>. If the function fails, the return value is <c>False</c>.
        ''' To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        ''' </returns>
        ''' <remarks>
        ''' For more details, refer to the <see href="https://docs.microsoft.com/en-us/windows/desktop/api/processthreadsapi/nf-processthreadsapi-openprocesstoken">OpenProcessToken</see> documentation.
        ''' 
        ''' The function signature in C++ is:
        ''' <code>
        ''' BOOL OpenProcessToken(
        '''   [in]  HANDLE  ProcessHandle,
        '''   [in]  DWORD   DesiredAccess,
        '''   [out] PHANDLE TokenHandle
        ''' );
        ''' </code>
        ''' </remarks>
        <DllImport(ExternDll.Advapi32, SetLastError:=True)>
        Friend Shared Function OpenProcessToken(
            <[In]> processHandle As IntPtr,
            <[In]> desiredAccess As AccessMask,
            <[Out]> ByRef tokenHandle As IntPtr
        ) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ''' <summary>
        ''' Closes an open object handle.
        ''' </summary>
        ''' <param name="hObject">
        ''' A valid handle to an open object. This handle is typically obtained from functions like <c>CreateFile</c>,
        ''' <c>OpenProcess</c>, or <c>OpenThread</c>. This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <returns>
        ''' If the function succeeds, the return value is nonzero (<c>True</c>). If the function fails, the return value is
        ''' zero (<c>False</c>). To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        ''' </returns>
        ''' <remarks>
        ''' The <c>CloseHandle</c> function is used to close an open handle to an object. It is crucial to call this function
        ''' to free system resources when a handle is no longer needed.
        ''' 
        ''' For more details, refer to the <see href="https://learn.microsoft.com/en-us/windows/desktop/api/handleapi/nf-handleapi-closehandle">CloseHandle</see> documentation.
        ''' 
        ''' The function signature in C++ is:
        ''' <code>
        ''' BOOL CloseHandle(
        '''   [in] HANDLE hObject
        ''' );
        ''' </code>
        ''' </remarks>
        <DllImport(ExternDll.Kernel32, SetLastError:=True)>
        Friend Shared Function CloseHandle(
            <[In]> hObject As IntPtr
        ) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ''' <summary>
        ''' Retrieves the session identifier of the console session. The console session is the session that is currently
        ''' attached to the physical console.
        ''' </summary>
        ''' <returns>
        ''' The session identifier of the session that is attached to the physical console. If there is no session attached to
        ''' the physical console (for example, if the physical console session is in the process of being attached or detached),
        ''' this function returns <c>0xFFFFFFFF</c>.
        ''' </returns>
        ''' <remarks>
        ''' This function does not require Remote Desktop Services to be running to succeed.
        ''' 
        ''' For more information, refer to the <see href="https://learn.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-wtsgetactiveconsolesessionid">WTSGetActiveConsoleSessionId</see> documentation.
        ''' 
        ''' The function signature in C++ is:
        ''' <code>
        ''' DWORD WTSGetActiveConsoleSessionId();
        ''' </code>
        ''' </remarks>
        <DllImport(ExternDll.Kernel32)>
        Friend Shared Function WTSGetActiveConsoleSessionId() As UInteger
        End Function

        ''' <summary>
        ''' Creates a new access token that duplicates an existing token. This function can create either a primary token or an impersonation token.
        ''' </summary>
        ''' <param name="hExistingToken">
        ''' A handle to an access token opened with <c>TOKEN_DUPLICATE</c> access. This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <param name="dwDesiredAccess">
        ''' Specifies the requested access rights for the new token. The <c>DuplicateTokenEx</c> function compares the requested access rights
        ''' with the existing token's discretionary access control list (DACL) to determine which rights are granted or denied. To request the same
        ''' access rights as the existing token, specify zero. To request all access rights that are valid for the caller, specify <c>MAXIMUM_ALLOWED</c>.
        ''' This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <param name="lpTokenAttributes">
        ''' A pointer to a <see cref="SecurityAttributes"/> structure that specifies a security descriptor for the new token and determines whether
        ''' child processes can inherit the token. If <c>lpTokenAttributes</c> is <c>Nothing</c>, the token gets a default security descriptor and the handle
        ''' cannot be inherited. If the security descriptor contains a system access control list (SACL), the token gets <c>ACCESS_SYSTEM_SECURITY</c>
        ''' access right, even if it was not requested in <c>dwDesiredAccess</c>. This parameter is passed with the <c>[In, Optional]</c> attribute.
        ''' </param>
        ''' <param name="impersonationLevel">
        ''' Specifies a value from the <c>SECURITY_IMPERSONATION_LEVEL</c> enumeration that indicates the impersonation level of the new token. This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <param name="tokenType">
        ''' Specifies one of the values from the <c>TOKEN_TYPE</c> enumeration. This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <param name="phNewToken">
        ''' A pointer to a handle that receives the new token. This handle is used to identify the token. This parameter is passed with the <c>[Out]</c> attribute.
        ''' </param>
        ''' <returns>
        ''' If the function succeeds, the function returns a nonzero value. If it fails, it returns zero. Use <see cref="Marshal.GetLastWin32Error"/> to get extended error information.
        ''' </returns>
        ''' <remarks>
        ''' The function signature in C++ is:
        ''' <code>
        ''' BOOL DuplicateTokenEx(
        '''   [in]           HANDLE                       hExistingToken,
        '''   [in]           DWORD                        dwDesiredAccess,
        '''   [in, optional] LPSECURITY_ATTRIBUTES        lpTokenAttributes,
        '''   [in]           SECURITY_IMPERSONATION_LEVEL ImpersonationLevel,
        '''   [in]           TOKEN_TYPE                   TokenType,
        '''   [out]          PHANDLE                      phNewToken
        ''' );
        ''' </code>
        ''' For more details, refer to the <see href="https://learn.microsoft.com/en-us/windows/desktop/api/securitybaseapi/nf-securitybaseapi-duplicatetokenex">DuplicateTokenEx</see> documentation.
        ''' </remarks>
        <DllImport(ExternDll.Advapi32)>
        Friend Shared Function DuplicateTokenEx(
             <[In]> hExistingToken As IntPtr,
             <[In]> dwDesiredAccess As UInteger,
             <[In], [Optional]> lpTokenAttributes As SecurityAttributes,
             <[In]> impersonationLevel As Integer,
             <[In]> tokenType As Integer,
             <[Out]> ByRef phNewToken As IntPtr
        ) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ''' <summary>
        ''' Creates a new process and its primary thread. The new process runs in the security context of the user represented
        ''' by the specified token.
        ''' </summary>
        ''' <param name="hToken">
        ''' A handle to the primary token that represents a user. The handle must have <c>TOKEN_QUERY</c>, <c>TOKEN_DUPLICATE</c>, and
        ''' <c>TOKEN_ASSIGN_PRIMARY</c> access rights. For more information, see Access Rights for Access-Token Objects. The user represented
        ''' by the token must have read and execute access to the application specified by the <paramref name="lpApplicationName"/> or the <paramref name="lpCommandLine"/> parameter.
        ''' This parameter is passed with the <c>[In], [Optional]</c> attribute.
        ''' </param>
        ''' <param name="lpApplicationName">
        ''' The name of the module to be executed. This module can be a Windows-based application. It can be some other type of module
        ''' (for example, MS-DOS or OS/2) if the appropriate subsystem is available on the local computer. This parameter is passed with the <c>[In], [Optional]</c> attribute.
        ''' </param>
        ''' <param name="lpCommandLine">
        ''' The command line to be executed. The maximum length of this string is 32K characters. If <paramref name="lpApplicationName"/> is <c>Nothing</c>,
        ''' the module name portion of <paramref name="lpCommandLine"/> is limited to <c>MAX_PATH</c> characters.
        ''' <para>
        ''' Note: Applying <c>[In], [Out], [Optional]</c> attributes to <c>lpCommandLine</c> may cause issues due to the way .NET handles
        ''' string marshalling and memory management when interfacing with unmanaged code. Specifically, <c>[In]</c> indicates that the parameter is 
        ''' being passed to the function, <c>[Out]</c> indicates that the function can modify the parameter, and <c>[Optional]</c> signifies that the
        ''' parameter is not required. Using these attributes together with <c>ByRef</c> can lead to unexpected behavior or runtime errors.
        ''' To avoid such problems, the parameter is kept as <c>lpCommandLine As String</c>, which simplifies marshaling and avoids potential issues.
        ''' This behavior goes beyond typical P/Invoke usage and requires careful consideration to ensure compatibility and correct operation.
        ''' </para>
        ''' This parameter is passed with the <c>[In]</c> and <c>[Out]</c> attributes.
        ''' </param>
        ''' <param name="lpProcessAttributes">
        ''' A pointer to a <see cref="SecurityAttributes"/> structure that specifies a security descriptor for the new process object and determines
        ''' whether child processes can inherit the returned handle to the process. If <c>lpProcessAttributes</c> is <c>Nothing</c> or <c>lpSecurityDescriptor</c> is <c>Nothing</c>,
        ''' the process gets a default security descriptor and the handle cannot be inherited. The default security descriptor is that of the user
        ''' referenced in the <c>hToken</c> parameter. This security descriptor may not allow access for the caller, in which case the process may not be
        ''' opened again after it is run. The process handle is valid and will continue to have full access rights.
        ''' This parameter is passed with the <c>[In], [Optional]</c> attribute.
        ''' </param>
        ''' <param name="lpThreadAttributes">
        ''' A pointer to a <see cref="SecurityAttributes"/> structure that specifies a security descriptor for the new thread object and determines
        ''' whether child processes can inherit the returned handle to the thread. If <c>lpThreadAttributes</c> is <c>Nothing</c> or <c>lpSecurityDescriptor</c> is <c>Nothing</c>,
        ''' the thread gets a default security descriptor and the handle cannot be inherited. The default security descriptor is that of the user
        ''' referenced in the <c>hToken</c> parameter. This security descriptor may not allow access for the caller.
        ''' This parameter is passed with the <c>[In], [Optional]</c> attribute.
        ''' </param>
        ''' <param name="bInheritHandle">
        ''' If this parameter is <c>TRUE</c>, each inheritable handle in the calling process is inherited by the new process. If the parameter is <c>FALSE</c>,
        ''' the handles are not inherited. Note that inherited handles have the same value and access rights as the original handles.
        ''' This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <param name="dwCreationFlags">
        ''' The flags that control the priority class and the creation of the process. For a list of values, see Process Creation Flags.
        ''' This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <param name="lpEnvironment">
        ''' A pointer to an environment block for the new process. If this parameter is <c>Nothing</c>, the new process uses the environment of the calling process.
        ''' This parameter is passed with the <c>[In], [Optional]</c> attribute.
        ''' </param>
        ''' <param name="lpCurrentDirectory">
        ''' The full path to the current directory for the process. The string can also specify a UNC path.
        ''' This parameter is passed with the <c>[In], [Optional]</c> attribute.
        ''' </param>
        ''' <param name="lpStartupInfo">
        ''' A pointer to a <see cref="StartupInfoA"/> or <c>StartupInfoEx</c> structure that specifies the window station, desktop, standard handles, and other related information for the new process.
        ''' This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <param name="lpProcessInformation">
        ''' A pointer to a <see cref="ProcessInformation"/> structure that receives identification information about the new process. This structure includes the handles for the new process and its primary thread.
        ''' This parameter is passed with the <c>[Out]</c> attribute.
        ''' </param>
        ''' <returns>
        ''' If the function succeeds, the return value is nonzero. If it fails, it returns zero. Use <see cref="Marshal.GetLastWin32Error"/> to obtain extended error information.
        ''' </returns>
        ''' <remarks>
        ''' For more details, refer to the <see href="https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-createprocessasusera">CreateProcessAsUserA</see> documentation.
        ''' 
        ''' The function signature in C++ is:
        ''' <code>
        ''' BOOL CreateProcessAsUserA(
        '''   [in, optional]      HANDLE                hToken,
        '''   [in, optional]      LPCSTR                lpApplicationName,
        '''   [in, out, optional] LPSTR                 lpCommandLine,
        '''   [in, optional]      LPSECURITY_ATTRIBUTES lpProcessAttributes,
        '''   [in, optional]      LPSECURITY_ATTRIBUTES lpThreadAttributes,
        '''   [in]                BOOL                  bInheritHandles,
        '''   [in]                DWORD                 dwCreationFlags,
        '''   [in, optional]      LPVOID                lpEnvironment,
        '''   [in, optional]      LPCSTR                lpCurrentDirectory,
        '''   [in]                LPSTARTUPINFOA        lpStartupInfo,
        '''   [out]               LPPROCESS_INFORMATION lpProcessInformation
        ''' );
        ''' </code>
        ''' </remarks>
        <SuppressMessage("Marshalling", "CA2101:Specify marshalling for p/invoke string arguments", Justification:="CharSet.Unicode and CharSet.Auto cause issues in this project. CharSet.Ansi has been verified to work correctly.")>
        <DllImport(ExternDll.Advapi32, SetLastError:=True, CharSet:=CharSet.Ansi)>
        Friend Shared Function CreateProcessAsUser(
            <[In], [Optional]> hToken As IntPtr,
            <[In], [Optional]> lpApplicationName As String,
            lpCommandLine As String,
            <[In], [Optional]> ByRef lpProcessAttributes As SecurityAttributes,
            <[In], [Optional]> ByRef lpThreadAttributes As SecurityAttributes,
            <[In]> <MarshalAs(UnmanagedType.Bool)> bInheritHandle As Boolean,
            <[In]> dwCreationFlags As UInteger,
            <[In], [Optional]> lpEnvironment As IntPtr,
            <[In], [Optional]> lpCurrentDirectory As String,
            <[In]> ByRef lpStartupInfo As StartupInfoA,
            <[Out]> ByRef lpProcessInformation As ProcessInformation
        ) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ''' <summary>
        ''' Retrieves information about the specified process.
        ''' </summary>
        ''' <param name="processHandle">
        ''' A handle to the process for which information is to be retrieved. The handle must have the appropriate access rights to the process.
        ''' This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <param name="processInformationClass">
        ''' The type of process information to be retrieved. This parameter can be one of the values from the 
        ''' <c>PROCESSINFOCLASS</c> enumeration, which defines various types of process information.
        ''' This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <param name="processInformation">
        ''' A pointer to a buffer supplied by the calling application into which the function writes the requested information. 
        ''' The size of the information written varies depending on the data type of the <paramref name="processInformationClass"/> parameter.
        ''' This parameter is passed with the <c>[Out]</c> attribute.
        ''' </param>
        ''' <param name="processInformationLength">
        ''' The size of the buffer pointed to by the <paramref name="processInformation"/> parameter, in bytes.
        ''' This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <param name="returnLength">
        ''' A pointer to a variable in which the function returns the size of the requested information. If the function was successful, 
        ''' this is the size of the information written to the buffer pointed to by the <paramref name="processInformation"/> parameter. 
        ''' If the buffer was too small, this is the minimum size of buffer needed to receive the information successfully.
        ''' This parameter is passed with the <c>[Out], [Optional]</c> attribute.
        ''' </param>
        ''' <returns>
        ''' The function returns an NTSTATUS success or error code. The forms and significance of NTSTATUS error codes are listed in the Ntstatus.h header file available in the DDK, and are 
        ''' described in the DDK documentation under Kernel-Mode Driver Architecture / Design Guide / Driver Programming Techniques / 
        ''' Logging Errors.
        ''' </returns>
        ''' <remarks>
        ''' The function signature in C++ is:
        ''' <code>
        ''' __kernel_entry NTSTATUS NtQueryInformationProcess(
        '''   [in]            HANDLE           ProcessHandle,
        '''   [in]            PROCESSINFOCLASS ProcessInformationClass,
        '''   [out]           PVOID            ProcessInformation,
        '''   [in]            ULONG            ProcessInformationLength,
        '''   [out, optional] PULONG           ReturnLength
        ''' );
        ''' </code>
        ''' 
        ''' <para>
        ''' Although this signature is not used directly in this project, it is kept for completeness and historical reasons, and may 
        ''' be referenced or used in other contexts related to process management. For historical purposes, it is important to note the 
        ''' association with the <see cref="ProcessAccessRights"/> class, which may be relevant for understanding access control in process-related operations.
        ''' </para>
        ''' 
        ''' For more information, refer to the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winternl/nf-winternl-ntqueryinformationprocess">NtQueryInformationProcess</see> documentation.
        ''' </remarks>
        <DllImport(ExternDll.Ntdll)>
        Friend Shared Function NtQueryInformationProcess(
            <[In]> processHandle As IntPtr,
            <[In]> processInformationClass As Integer,
            <[Out]> ByRef processInformation As ProcessBasicInformation,
            <[In]> processInformationLength As Integer,
            <[Out], [Optional]> ByRef returnLength As Integer
        ) As Integer
        End Function
    End Class
End Namespace

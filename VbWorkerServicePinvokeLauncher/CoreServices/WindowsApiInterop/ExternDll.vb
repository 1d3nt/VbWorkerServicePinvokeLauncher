Namespace CoreServices.WindowsApiInterop

    ''' <summary>
    ''' Provides constants for the names of various dynamic link libraries (DLLs).
    ''' </summary>
    ''' <remarks>
    ''' The <see cref="ExternDll"/> class provides a centralized location for the names of external DLLs that are commonly used in P/Invoke calls.
    ''' This helps in avoiding hardcoding the DLL names throughout the codebase, making it easier to maintain and update if needed.
    ''' </remarks>
    Friend NotInheritable Class ExternDll

        ''' <devdoc>
        '''    <para>
        '''       Specifies that the
        '''       Kernel32.dll is dynamic link library
        '''    </para>
        ''' </devdoc>
        Friend Const Kernel32 As String = "kernel32.dll"

        ''' <devdoc>
        '''    <para>
        '''       Specifies that the
        '''       advapi32.dll is dynamic link library
        '''    </para>
        ''' </devdoc>
        Friend Const Advapi32 As String = "advapi32.dll"

        ''' <devdoc>
        '''    <para>
        '''       Specifies that the
        '''       ntdll.dll is dynamic link library
        '''    </para>
        ''' </devdoc>
        Friend Const Ntdll As String = "ntdll.dll"
    End Class
End Namespace


Namespace Structures
    ''' <summary>
    '''     The SECURITY_ATTRIBUTES structure contains the security descriptor for an object and specifies
    '''     whether the handle retrieved by specifying this structure is inheritable. This structure provides
    '''     security settings for objects created by various functions, such as CreateFile, CreatePipe,
    '''     CreateProcess, RegCreateKeyEx, or RegSaveKeyEx.
    ''' </summary>
    ''' <remarks>
    '''     See https://msdn.microsoft.com/en-us/library/windows/desktop/aa379560(v=vs.85).aspx
    ''' </remarks>
    <StructLayout(LayoutKind.Sequential)>
    Public Structure SecurityAttributes
        Public nLength As Integer
        Public lpSecurityDescriptor As IntPtr
        Public bInheritHandle As Boolean
    End Structure
End Namespace

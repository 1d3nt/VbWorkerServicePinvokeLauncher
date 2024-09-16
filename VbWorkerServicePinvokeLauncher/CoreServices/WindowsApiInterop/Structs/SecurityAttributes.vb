Namespace CoreServices.WindowsApiInterop.Structs

    ''' <summary>
    ''' The SECURITY_ATTRIBUTES structure contains the security descriptor for an object and specifies
    ''' whether the handle retrieved by specifying this structure is inheritable. This structure provides
    ''' security settings for objects created by various functions, such as CreateFile, CreatePipe,
    ''' CreateProcess, RegCreateKeyEx, or RegSaveKeyEx.
    ''' </summary>
    ''' <remarks>
    ''' The <see cref="SecurityAttributes"/> structure corresponds to the <c>SECURITY_ATTRIBUTES</c> structure in the Windows API:
    ''' <code>
    ''' typedef struct _SECURITY_ATTRIBUTES {
    '''   DWORD  nLength;
    '''   LPVOID lpSecurityDescriptor;
    '''   BOOL   bInheritHandle;
    ''' } SECURITY_ATTRIBUTES, *PSECURITY_ATTRIBUTES, *LPSECURITY_ATTRIBUTES;
    ''' </code>
    ''' 
    ''' The layout of this structure is sequential, which means the fields are laid out in memory in the same order as they are declared.
    ''' 
    ''' The packing size for this structure is set to 4 bytes. This matches the default packing size in .NET, which aligns with the 
    ''' alignment requirements for the <c>SECURITY_ATTRIBUTES</c> structure as used by the Windows API. This is because <c>DWORD</c>, 
    ''' <c>LPVOID</c>, and <c>BOOL</c> are all 4 bytes in size.
    ''' 
    ''' For additional details on the <c>SECURITY_ATTRIBUTES</c> structure, refer to:
    ''' <see href="https://learn.microsoft.com/en-us/windows/win32/api/wtypesbase/ns-wtypesbase-security_attributes">SECURITY_ATTRIBUTES Structure</see>.
    ''' </remarks>
    <StructLayout(LayoutKind.Sequential)>
    Friend Structure SecurityAttributes

        ''' <summary>
        ''' The size of the structure, in bytes.
        ''' </summary>
        Friend nLength As Integer

        ''' <summary>
        ''' A pointer to a security descriptor for the object.
        ''' </summary>
        Friend lpSecurityDescriptor As IntPtr

        ''' <summary>
        ''' A boolean value that specifies whether the handle is inheritable.
        ''' </summary>
        Friend bInheritHandle As Boolean
    End Structure
End Namespace

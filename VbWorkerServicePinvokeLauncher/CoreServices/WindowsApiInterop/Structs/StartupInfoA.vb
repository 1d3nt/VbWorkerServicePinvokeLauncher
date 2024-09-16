Namespace CoreServices.WindowsApiInterop.Structs

    ''' <summary>
    ''' Specifies the window station, desktop, standard handles, and appearance of the main 
    ''' window for a process at creation time.
    ''' </summary>
    ''' <remarks>
    ''' This structure is used to specify various attributes related to the window station, desktop, 
    ''' and handles for a process when it is created. It is utilized by the <c>CreateProcess</c> function 
    ''' and similar functions to control how the process's main window should appear and interact with the system.
    ''' 
    ''' The structure corresponds to the C++ definition:
    ''' <code>
    ''' typedef struct _STARTUPINFOA {
    '''   DWORD  cb;
    '''   LPSTR  lpReserved;
    '''   LPSTR  lpDesktop;
    '''   LPSTR  lpTitle;
    '''   DWORD  dwX;
    '''   DWORD  dwY;
    '''   DWORD  dwXSize;
    '''   DWORD  dwYSize;
    '''   DWORD  dwXCountChars;
    '''   DWORD  dwYCountChars;
    '''   DWORD  dwFillAttribute;
    '''   DWORD  dwFlags;
    '''   WORD   wShowWindow;
    '''   WORD   cbReserved2;
    '''   LPBYTE lpReserved2;
    '''   HANDLE hStdInput;
    '''   HANDLE hStdOutput;
    '''   HANDLE hStdError;
    ''' } STARTUPINFOA, *LPSTARTUPINFOA;
    ''' </code>
    ''' 
    ''' The <c>LayoutKind.Sequential</c> attribute ensures that the fields are laid out in memory in 
    ''' the same order as they are defined in the structure, which matches the C++ layout.
    ''' 
    ''' <c>Pack</c> is not specified here because the default packing size of 4 bytes is appropriate 
    ''' for this structure. This is due to the sizes of the fields (e.g., DWORD, WORD) and their alignment 
    ''' requirements, which match the Windows API expectations for the structure.
    ''' 
    ''' For more detailed information about this structure, refer to:
    ''' <see href="https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/ns-processthreadsapi-startupinfoa">STARTUPINFOA Structure</see>.
    ''' </remarks>
    <StructLayout(LayoutKind.Sequential)>
    Public Structure StartupInfoA

        ''' <summary>
        ''' The size of the structure, in bytes. This should be set to the size of the structure.
        ''' </summary>
        Public cb As Integer

        ''' <summary>
        ''' Reserved; should be set to <c>NULL</c>.
        ''' </summary>
        Public lpReserved As String

        ''' <summary>
        ''' The name of the desktop. If <c>NULL</c>, the process uses the desktop of the calling thread.
        ''' </summary>
        Public lpDesktop As String

        ''' <summary>
        ''' The title of the main window. If <c>NULL</c>, the process title is the name of the executable file.
        ''' </summary>
        Public lpTitle As String

        ''' <summary>
        ''' The x-coordinate, in pixels, of the upper-left corner of the window. This value is used only if the 
        ''' <c>STARTF_USEPOSITION</c> flag is specified in the <c>dwFlags</c> member.
        ''' </summary>
        Public dwX As UInteger

        ''' <summary>
        ''' The y-coordinate, in pixels, of the upper-left corner of the window. This value is used only if the 
        ''' <c>STARTF_USEPOSITION</c> flag is specified in the <c>dwFlags</c> member.
        ''' </summary>
        Public dwY As UInteger

        ''' <summary>
        ''' The width, in pixels, of the window. This value is used only if the <c>STARTF_USESIZE</c> flag is specified 
        ''' in the <c>dwFlags</c> member.
        ''' </summary>
        Public dwXSize As UInteger

        ''' <summary>
        ''' The height, in pixels, of the window. This value is used only if the <c>STARTF_USESIZE</c> flag is specified 
        ''' in the <c>dwFlags</c> member.
        ''' </summary>
        Public dwYSize As UInteger

        ''' <summary>
        ''' The number of character columns in the window. This value is used only if the <c>STARTF_USECOUNTCHARS</c> 
        ''' flag is specified in the <c>dwFlags</c> member.
        ''' </summary>
        Public dwXCountChars As UInteger

        ''' <summary>
        ''' The number of character rows in the window. This value is used only if the <c>STARTF_USECOUNTCHARS</c> 
        ''' flag is specified in the <c>dwFlags</c> member.
        ''' </summary>
        Public dwYCountChars As UInteger

        ''' <summary>
        ''' The text and background color attributes of the window. This value is used only if the 
        ''' <c>STARTF_USEFILLATTRIBUTE</c> flag is specified in the <c>dwFlags</c> member.
        ''' </summary>
        Public dwFillAttribute As UInteger

        ''' <summary>
        ''' A set of flags that specify which window attributes to use. This value can be a combination of 
        ''' the <c>STARTF_</c> constants.
        ''' </summary>
        Public dwFlags As UInteger

        ''' <summary>
        ''' Controls how the window is to be shown. This value can be one of the <c>SW_</c> constants.
        ''' </summary>
        Public wShowWindow As Short

        ''' <summary>
        ''' The size of the reserved area for additional information. This value should be set to the size of 
        ''' <c>lpReserved2</c>.
        ''' </summary>
        Public cbReserved2 As Short

        ''' <summary>
        ''' A pointer to additional reserved information. This value should be set to <c>NULL</c>.
        ''' </summary>
        Public lpReserved2 As IntPtr

        ''' <summary>
        ''' A handle to the standard input device for the process. If <c>NULL</c>, the process uses the input 
        ''' device of the parent process.
        ''' </summary>
        Public hStdInput As IntPtr

        ''' <summary>
        ''' A handle to the standard output device for the process. If <c>NULL</c>, the process uses the output 
        ''' device of the parent process.
        ''' </summary>
        Public hStdOutput As IntPtr

        ''' <summary>
        ''' A handle to the standard error device for the process. If <c>NULL</c>, the process uses the error 
        ''' device of the parent process.
        ''' </summary>
        Public hStdError As IntPtr
    End Structure
End Namespace

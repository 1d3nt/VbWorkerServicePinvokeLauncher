Namespace Structures
    ''' <summary>
    '''     Specifies the window station, desktop, standard handles, and appearance of the main 
    '''     window for a process at creation time.
    ''' </summary>
    ''' <remarks>
    '''    See https://docs.microsoft.com/en-us/windows/desktop/api/processthreadsapi/ns-processthreadsapi-_startupinfoa
    ''' </remarks>
    <StructLayout(LayoutKind.Sequential)>
    Public Structure StartupInfoA
        Public cb As Integer
        Public lpReserved As String
        Public lpDesktop As String
        Public lpTitle As String
        Public dwX As UInteger
        Public dwY As UInteger
        Public dwXSize As UInteger
        Public dwYSize As UInteger
        Public dwXCountChars As UInteger
        Public dwYCountChars As UInteger
        Public dwFillAttribute As UInteger
        Public dwFlags As UInteger
        Public wShowWindow As Short
        Public cbReserved2 As Short
        Public lpReserved2 As IntPtr
        Public hStdInput As IntPtr
        Public hStdOutput As IntPtr
        Public hStdError As IntPtr
    End Structure
End Namespace

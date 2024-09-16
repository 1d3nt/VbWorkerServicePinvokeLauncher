Namespace CoreServices.WindowsApiInterop.Structs

    ''' <summary>
    ''' Contains information about a newly created process and its primary thread.
    ''' It is used with the <c>CreateProcess</c>, <c>CreateProcessAsUser</c>, <c>CreateProcessWithLogonW</c>, or <c>CreateProcessWithTokenW</c> function.
    ''' </summary>
    ''' <remarks>
    ''' The <see cref="ProcessInformation"/> structure is used by various Windows API functions to provide information about 
    ''' the newly created process and its primary thread. This includes handles to the process and thread, as well as their IDs.
    ''' 
    ''' For more detailed information about this structure, refer to the following documentation:
    ''' <see href="https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/ns-processthreadsapi-process_information">PROCESS_INFORMATION Structure</see>.
    ''' 
    ''' The following is the C++ definition for the <c>PROCESS_INFORMATION</c> structure:
    ''' <code>
    ''' typedef struct _PROCESS_INFORMATION {
    '''   HANDLE hProcess;
    '''   HANDLE hThread;
    '''   DWORD  dwProcessId;
    '''   DWORD  dwThreadId;
    ''' } PROCESS_INFORMATION, *PPROCESS_INFORMATION, *LPPROCESS_INFORMATION;
    ''' </code>
    ''' 
    ''' The <see cref="ProcessInformation"/> structure uses <c>LayoutKind.Sequential</c> to ensure that the fields are laid out
    ''' in the same order as defined in the structure. The <c>Pack</c> attribute is set to 4 to match the typical alignment used
    ''' by the Windows API for this structure. This ensures compatibility with the memory layout expected by the API.
    ''' 
    ''' For additional details, see the corresponding C++ structure definition and the API documentation linked above.
    ''' </remarks>
    <StructLayout(LayoutKind.Sequential, Pack:=4)>
    Friend Structure ProcessInformation

        ''' <summary>
        ''' A handle to the newly created process.
        ''' </summary>
        Friend hProcess As IntPtr

        ''' <summary>
        ''' A handle to the primary thread of the newly created process.
        ''' </summary>
        Friend hThread As IntPtr

        ''' <summary>
        ''' The process identifier (PID) of the newly created process.
        ''' </summary>
        Friend dwProcessId As UInteger

        ''' <summary>
        ''' The thread identifier (TID) of the primary thread of the newly created process.
        ''' </summary>
        Friend dwThreadId As UInteger
    End Structure
End Namespace

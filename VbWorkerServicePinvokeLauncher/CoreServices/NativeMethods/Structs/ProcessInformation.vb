Imports System.Runtime.InteropServices

Namespace Structures
    ''' <summary>
    '''     Contains information about a newly created process and its primary thread. It is used with
    '''     the CreateProcess, CreateProcessAsUser, CreateProcessWithLogonW, or CreateProcessWithTokenW function.
    ''' </summary>
    ''' <remarks>
    '''     See
    '''     https://docs.microsoft.com/en-us/windows/desktop/api/processthreadsapi/ns-processthreadsapi-_process_information
    ''' </remarks>
    <StructLayout(LayoutKind.Sequential)>
    Public Structure ProcessInformation
        Public hProcess As IntPtr
        Public hThread As IntPtr
        Public dwProcessId As UInteger
        Public dwThreadId As UInteger
    End Structure
End Namespace


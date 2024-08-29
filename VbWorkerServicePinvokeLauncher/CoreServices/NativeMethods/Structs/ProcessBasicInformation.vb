Namespace Classes.Processes.Structures
    ''' <summary>
    '''     Retrieves information about the specified process.
    ''' </summary>
    ''' <remarks>
    '''     See https://docs.microsoft.com/en-us/windows/desktop/api/winternl/nf-winternl-ntqueryinformationprocess
    ''' </remarks>
    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Friend Structure ProcessBasicInformation
        Friend ExitStatus As IntPtr
        Friend PebBaseAddress As IntPtr
        Friend AffinityMask As IntPtr
        Friend BasePriority As IntPtr
        Friend UniqueProcessId As IntPtr
        Friend InheritedFromUniqueProcessId As IntPtr
    End Structure
End Namespace

Namespace CoreServices.NativeMethods.Structs

    ''' <summary>
    ''' Contains information about a specified process, retrieved by the <see cref="Methods.NativeMethods.NtQueryInformationProcess"/> function.
    ''' </summary>
    ''' <remarks>
    ''' The <see cref="ProcessBasicInformation"/> structure is used by the <see cref="Methods.NativeMethods.NtQueryInformationProcess"/> function
    ''' to provide various details about a process. This includes the process's exit status, base address of the 
    ''' Process Environment Block (PEB), process affinity mask, base priority, unique process ID, and the ID of 
    ''' the process from which this process was inherited.
    ''' 
    ''' This structure corresponds to the <c>PROCESS_BASIC_INFORMATION</c> structure in the Windows API:
    ''' <c>typedef struct _PROCESS_BASIC_INFORMATION {
    '''     NTSTATUS ExitStatus;
    '''     PPEB PebBaseAddress;
    '''     ULONG_PTR AffinityMask;
    '''     KPRIORITY BasePriority;
    '''     ULONG_PTR UniqueProcessId;
    '''     ULONG_PTR InheritedFromUniqueProcessId;
    ''' } PROCESS_BASIC_INFORMATION;</c>
    ''' 
    ''' The <see cref="ProcessBasicInformation"/> structure uses <c>LayoutKind.Sequential</c> to ensure that the fields are laid out
    ''' in the same order as defined in the structure. The <c>Pack:=8</c> attribute is applied to align the structure's fields
    ''' on an 8-byte boundary, which matches the expected alignment for 64-bit systems and ensures compatibility with the
    ''' unmanaged API.
    ''' 
    ''' Although the <see cref="Methods.NativeMethods.NtQueryInformationProcess"/> function is not currently used in the application,
    ''' it is included in this project for completeness and potential future use. This structure and associated function are retained
    ''' as part of the development for possible future integration.
    ''' 
    ''' For additional details on the <see cref="Methods.NativeMethods.NtQueryInformationProcess"/> function and this structure, refer to:
    ''' <see href="https://learn.microsoft.com/en-us/windows/desktop/api/winternl/nf-winternl-ntqueryinformationprocess">NtQueryInformationProcess</see>
    ''' and
    ''' <see href="https://learn.microsoft.com/en-us/windows/win32/api/winternl/ns-winternl-process_basic_information">PROCESS_BASIC_INFORMATION Structure</see>.
    ''' </remarks>
    <StructLayout(LayoutKind.Sequential, Pack:=8)>
    Friend Structure ProcessBasicInformation

        ''' <summary>
        ''' The exit status of the process.
        ''' </summary>
        Friend ExitStatus As IntPtr

        ''' <summary>
        ''' A pointer to the Process Environment Block (PEB) for the process.
        ''' </summary>
        Friend PebBaseAddress As IntPtr

        ''' <summary>
        ''' The process affinity mask, which specifies the processors on which the process is allowed to run.
        ''' </summary>
        Friend AffinityMask As IntPtr

        ''' <summary>
        ''' The base priority of the process.
        ''' </summary>
        Friend BasePriority As IntPtr

        ''' <summary>
        ''' The unique identifier of the process.
        ''' </summary>
        Friend UniqueProcessId As IntPtr

        ''' <summary>
        ''' The unique identifier of the process from which this process was inherited.
        ''' </summary>
        Friend InheritedFromUniqueProcessId As IntPtr
    End Structure
End Namespace

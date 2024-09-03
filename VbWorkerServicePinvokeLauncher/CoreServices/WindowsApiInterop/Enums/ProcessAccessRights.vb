Namespace CoreServices.WindowsApiInterop.Enums

    ''' <summary>
    ''' Represents various access rights that can be granted to a process.
    ''' This enum is included for completeness, although it is not currently utilized in the application.
    ''' </summary>
    ''' <remarks>
    ''' The <see cref="ProcessAccessRights"/> enum defines constants for various process access rights that can be granted to a process.
    ''' These rights are used in Windows API functions like <see cref="Methods.NativeMethods.OpenProcess"/> to specify access levels for process management.
    ''' 
    ''' Although there is no direct C++ enumeration for these values, they correspond to constants used in the Windows API:
    ''' <list type="bullet">
    '''     <item><description><see cref="Terminate"/> corresponds to <c>PROCESS_TERMINATE</c>.</description></item>
    '''     <item><description><see cref="CreateThread"/> corresponds to <c>PROCESS_CREATE_THREAD</c>.</description></item>
    '''     <item><description><see cref="VirtualMemoryOperation"/> corresponds to <c>PROCESS_VM_OPERATION</c>.</description></item>
    '''     <item><description><see cref="VirtualMemoryRead"/> corresponds to <c>PROCESS_VM_READ</c>.</description></item>
    '''     <item><description><see cref="DuplicateHandle"/> corresponds to <c>PROCESS_DUP_HANDLE</c>.</description></item>
    '''     <item><description><see cref="CreateProcess"/> corresponds to <c>PROCESS_CREATE_PROCESS</c>.</description></item>
    '''     <item><description><see cref="SetQuota"/> corresponds to <c>PROCESS_SET_QUOTA</c>.</description></item>
    '''     <item><description><see cref="SetInformation"/> corresponds to <c>PROCESS_SET_INFORMATION</c>.</description></item>
    '''     <item><description><see cref="QueryInformation"/> corresponds to <c>PROCESS_QUERY_INFORMATION</c>.</description></item>
    '''     <item><description><see cref="QueryLimitedInformation"/> corresponds to <c>PROCESS_QUERY_LIMITED_INFORMATION</c>.</description></item>
    '''     <item><description><see cref="Synchronize"/> corresponds to <c>PROCESS_SYNCHRONIZE</c>.</description></item>
    ''' </list>
    ''' 
    ''' For detailed information about these access rights, refer to:
    ''' <see href="https://learn.microsoft.com/en-us/windows/win32/procthread/process-security-and-access-rights">Process Security and Access Rights</see>.
    ''' Note: The <c>UsedImplicitly</c> attribute is applied to each member of the enum to suppress warnings about these members being unused. 
    ''' This ensures that the enum is kept complete for completeness and future extensibility, despite not being utilized directly in the current project.
    ''' </remarks>

    <UsedImplicitly, Flags>
    Friend Enum ProcessAccessRights As UInteger
        ''' <summary>
        ''' Grants all available access rights.
        ''' </summary>
        <UsedImplicitly>
        All = &H1F0FFF

        ''' <summary>
        ''' Grants the right to terminate a process.
        ''' </summary>
        <UsedImplicitly>
        Terminate = &H1

        ''' <summary>
        ''' Grants the right to create a thread in the process.
        ''' </summary>
        <UsedImplicitly>
        CreateThread = &H2

        ''' <summary>
        ''' Grants the right to perform virtual memory operations.
        ''' </summary>
        <UsedImplicitly>
        VirtualMemoryOperation = &H8

        ''' <summary>
        ''' Grants the right to read virtual memory.
        ''' </summary>
        <UsedImplicitly>
        VirtualMemoryRead = &H10

        ''' <summary>
        ''' Grants the right to duplicate a handle.
        ''' </summary>
        <UsedImplicitly>
        DuplicateHandle = &H40

        ''' <summary>
        ''' Grants the right to create a process in the process.
        ''' </summary>
        <UsedImplicitly>
        CreateProcess = &H80

        ''' <summary>
        ''' Grants the right to set quotas for the process.
        ''' </summary>
        <UsedImplicitly>
        SetQuota = &H100

        ''' <summary>
        ''' Grants the right to set process information.
        ''' </summary>
        <UsedImplicitly>
        SetInformation = &H200

        ''' <summary>
        ''' Grants the right to query process information.
        ''' </summary>
        <UsedImplicitly>
        QueryInformation = &H400

        ''' <summary>
        ''' Grants the right to query limited information about the process.
        ''' </summary>
        <UsedImplicitly>
        QueryLimitedInformation = &H1000

        ''' <summary>
        ''' Grants the right to synchronize with the process.
        ''' </summary>
        <UsedImplicitly>
        Synchronize = &H100000
    End Enum
End Namespace

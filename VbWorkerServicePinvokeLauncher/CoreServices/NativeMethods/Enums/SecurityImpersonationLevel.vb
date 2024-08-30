Namespace CoreServices.NativeMethods.Enums

    ''' <summary>
    ''' Specifies the security impersonation levels that control the degree to which a server process can act on behalf of a client process.
    ''' </summary>
    ''' <remarks>
    ''' The <see cref="SecurityImpersonationLevel"/> enum contains values that represent different levels of impersonation, allowing a server process to act on behalf of a client process to varying degrees.
    ''' For detailed information about impersonation levels, refer to:
    ''' <see href="https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-lsad/720cea10-cee2-4c45-9084-c6fa7d67d18d">SECURITY_IMPERSONATION_LEVEL Enumeration</see>.
    ''' 
    ''' This enum corresponds to the following C++ definition:
    ''' <c>typedef enum _SECURITY_IMPERSONATION_LEVEL
    ''' {
    '''   SecurityAnonymous = 0,
    '''   SecurityIdentification = 1,
    '''   SecurityImpersonation = 2,
    '''   SecurityDelegation = 3
    ''' } SECURITY_IMPERSONATION_LEVEL, *PSECURITY_IMPERSONATION_LEVEL;</c>
    ''' 
    ''' The <c>UsedImplicitly</c> attribute is applied to each member of the enum to suppress warnings about these members being unused. 
    ''' This ensures that the enum is kept complete for completeness and future extensibility, despite not being utilized directly in the current project.
    ''' </remarks>
    Friend Enum SecurityImpersonationLevel As Integer

        ''' <summary>
        ''' Allows the server to obtain the client’s identity, but does not allow the server to impersonate the client.
        ''' </summary>
        <UsedImplicitly>
        SecurityAnonymous = 0

        ''' <summary>
        ''' Allows the server to obtain the client’s identity and impersonate the client, but does not permit the server to act on behalf of the client.
        ''' </summary>
        SecurityIdentification = 1

        ''' <summary>
        ''' Allows the server to impersonate the client and act on behalf of the client.
        ''' </summary>
        <UsedImplicitly>
        SecurityImpersonation = 2

        ''' <summary>
        ''' Allows the server to impersonate the client and delegate client credentials to other servers.
        ''' </summary>
        <UsedImplicitly>
        SecurityDelegation = 3
    End Enum
End Namespace

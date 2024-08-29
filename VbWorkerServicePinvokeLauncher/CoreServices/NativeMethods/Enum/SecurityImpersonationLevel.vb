Namespace Enums
    ''' <summary>
    '''     The SECURITY_IMPERSONATION_LEVEL enumeration contains values that specify security impersonation levels.
    '''     Security impersonation levels govern the degree to which a server process can act on behalf of a client
    '''     process.
    ''' </summary>
    ''' <remarks>
    '''     See https://docs.microsoft.com/en-us/windows/desktop/api/winnt/ne-winnt-_security_impersonation_level
    ''' </remarks>
    Public Enum SecurityImpersonationLevel As Integer
        SecurityIdentification = 1
    End Enum
End Namespace

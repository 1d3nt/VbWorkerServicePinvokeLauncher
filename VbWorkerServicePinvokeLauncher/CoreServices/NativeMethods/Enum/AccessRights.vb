Namespace Enums
    ''' <summary>
    '''     An application cannot change the access control list of an object unless the application has the rights to do so.
    '''     These rights are controlled by a security descriptor in the access token for the object. For more information about
    '''     security, see Access Control Model.
    ''' </summary>
    ''' <remarks>
    '''     See https://docs.microsoft.com/en-us/windows/desktop/secauthz/access-rights-for-access-token-objects
    ''' </remarks>
    <Flags>
    Public Enum AccessRights As UInteger
        QueryInformation = &H400
        TokenDuplicate = &H2
        MaximumAllowed = &H2000000
        CreateNewConsole = &H10
        NormalPriorityClass = &H20
        StartFUsestdhandles = &H100
    End Enum
End Namespace
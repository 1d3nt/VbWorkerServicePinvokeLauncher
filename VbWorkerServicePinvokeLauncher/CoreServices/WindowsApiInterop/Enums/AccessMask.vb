Namespace CoreServices.WindowsApiInterop.Enums

    ''' <summary>
    ''' Represents the access rights that can be granted for access token objects.
    ''' An application cannot change the access control list of an object unless it has the necessary rights.
    ''' These rights are controlled by a security descriptor in the access token for the object.
    ''' </summary>
    ''' <remarks>
    ''' The <see cref="AccessMask"/> enum defines various access rights that can be granted to an access token.
    ''' These rights are used in Windows API functions for managing access to token objects and processes.
    ''' 
    ''' Although there is no direct C++ enumeration that matches these values, similar access rights are used in Windows API functions.
    ''' For example, in C++ code, you might use these constants with API functions such as <c>OpenProcessToken</c> or <c>CreateProcess</c>.
    ''' 
    ''' The following bullet points map the <see cref="AccessMask"/> enum values to their C++ equivalents:
    ''' <list type="bullet">
    '''     <item><description><see cref="TokenQuery"/> corresponds to the constant <c>TOKEN_QUERY</c> in C++.</description></item>
    '''     <item><description><see cref="QueryInformation"/> corresponds to the constant <c>TOKEN_QUERY_INFORMATION</c> in C++.</description></item>
    '''     <item><description><see cref="TokenDuplicate"/> corresponds to the constant <c>TOKEN_DUPLICATE</c> in C++.</description></item>
    '''     <item><description><see cref="MaximumAllowed"/> corresponds to the constant <c>MAXIMUM_ALLOWED</c> in C++.</description></item>
    '''     <item><description><see cref="CreateNewConsole"/> corresponds to the constant <c>TOKEN_CREATE_NEW_CONSOLE</c> in C++.</description></item>
    '''     <item><description><see cref="NormalPriorityClass"/> corresponds to setting the process priority class in C++ using the <c>PROCESS_SET_INFORMATION</c> and <c>PROCESS_PRIORITY_CLASS</c> constants.</description></item>
    '''     <item><description><see cref="StartFUseStdHandles"/> corresponds to setting handle inheritance in process creation functions.</description></item>
    '''     <item><description><see cref="QueryLimitedInformation"/> corresponds to the constant <c>PROCESS_QUERY_LIMITED_INFORMATION</c> in C++.</description></item>
    ''' </list>
    ''' 
    ''' <para>
    ''' Note: Although the <see cref="TokenQuery"/> value is not used in the current application, it is marked as <c>&lt;UsedImplicitly&gt;</c> 
    ''' and has been kept from another project for completeness and potential future use.
    ''' </para>
    ''' 
    ''' For more information about security and access control models, refer to:
    ''' <see href="https://learn.microsoft.com/windows/win32/secauthz/access-rights-for-access-token-objects">Access Rights for Access-Token Objects</see>.
    ''' </remarks>
    Friend Enum AccessMask As UInteger

        ''' <summary>
        ''' Grants the right to query an access token.
        ''' </summary>
        ''' <remarks>
        ''' This access right allows querying basic information about the access token, such as the user or group associated with the token.
        ''' It corresponds to the <c>TOKEN_QUERY</c> constant in C++.
        ''' 
        ''' <para>
        ''' <c>&lt;UsedImplicitly&gt;</c>: This entry was imported from another project. Although it might not be actively used in the current codebase, 
        ''' it has been retained to assist others who might need this constant in the future or for potential future use.
        ''' </para>
        ''' </remarks>
        <UsedImplicitly>
        TokenQuery = &H8

        ''' <summary>
        ''' Grants the right to query detailed information from an access token.
        ''' </summary>
        ''' <remarks>
        ''' This access right allows querying more detailed or sensitive information from an access token, such as privileges or owner details.
        ''' It corresponds to the <c>TOKEN_QUERY_INFORMATION</c> constant in C++.
        ''' </remarks>
        QueryInformation = &H400

        ''' <summary>
        ''' Grants the right to duplicate an access token.
        ''' </summary>
        ''' <remarks>
        ''' This access right allows creating a duplicate of the access token, which can be used to create a new process with the same security context.
        ''' It corresponds to the <c>TOKEN_DUPLICATE</c> constant in C++.
        ''' </remarks>
        TokenDuplicate = &H2

        ''' <summary>
        ''' Grants the right to obtain the maximum allowed permissions for a token.
        ''' </summary>
        ''' <remarks>
        ''' This access right allows obtaining the highest level of permissions available for the token.
        ''' It corresponds to the <c>MAXIMUM_ALLOWED</c> constant in C++.
        ''' </remarks>
        MaximumAllowed = &H2000000

        ''' <summary>
        ''' Grants the right to create a new console for the token.
        ''' </summary>
        ''' <remarks>
        ''' This access right allows creating a new console session for the token, typically used when starting a new process.
        ''' It corresponds to the <c>TOKEN_CREATE_NEW_CONSOLE</c> constant in C++.
        ''' </remarks>
        CreateNewConsole = &H10

        ''' <summary>
        ''' Sets the process priority class to normal.
        ''' </summary>
        ''' <remarks>
        ''' This access right allows setting the process priority class to normal when creating a process with this token.
        ''' </remarks>
        NormalPriorityClass = &H20

        ''' <summary>
        ''' Indicates that the process should inherit standard handles (stdin, stdout, stderr).
        ''' </summary>
        ''' <remarks>
        ''' This access right allows specifying that standard handles should be inherited by the process when created.
        ''' </remarks>
        StartFUseStdHandles = &H100

        ''' <summary>
        ''' Grants the right to query a limited set of information from a process.
        ''' </summary>
        ''' <remarks>
        ''' This access right allows querying a limited subset of information from a process, such as basic attributes.
        ''' It corresponds to the <c>PROCESS_QUERY_LIMITED_INFORMATION</c> constant in C++.
        ''' </remarks>
        QueryLimitedInformation = &H1000
    End Enum
End Namespace

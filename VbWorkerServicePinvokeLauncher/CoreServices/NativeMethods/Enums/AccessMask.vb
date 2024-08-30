Namespace CoreServices.NativeMethods.Enums

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
    '''     <item><description><c>QueryInformation</c> corresponds to the constant <c>TOKEN_QUERY</c> in C++.</description></item>
    '''     <item><description><c>TokenDuplicate</c> corresponds to the constant <c>TOKEN_DUPLICATE</c> in C++.</description></item>
    '''     <item><description><c>MaximumAllowed</c> corresponds to the constant <c>MAXIMUM_ALLOWED</c> in C++.</description></item>
    '''     <item><description><c>CreateNewConsole</c> is not directly mapped to a single C++ constant but is used with console-related APIs.</description></item>
    '''     <item><description><c>NormalPriorityClass</c> corresponds to setting the process priority class in C++ using the <c>PROCESS_SET_INFORMATION</c> and <c>PROCESS_PRIORITY_CLASS</c> constants.</description></item>
    '''     <item><description><c>StartFUseStdHandles</c> is not directly mapped to a single C++ constant but indicates handle inheritance in process creation functions.</description></item>
    ''' </list>
    ''' 
    ''' For more information about security and access control models, refer to:
    ''' <see href="https://learn.microsoft.com/windows/win32/secauthz/access-rights-for-access-token-objects">Access Rights for Access-Token Objects</see>.
    ''' </remarks>
    <Flags>
    Friend Enum AccessMask As UInteger

        ''' <summary>
        ''' Grants the right to query an access token.
        ''' </summary>
        QueryInformation = &H400

        ''' <summary>
        ''' Grants the right to duplicate an access token.
        ''' </summary>
        TokenDuplicate = &H2

        ''' <summary>
        ''' Grants the right to obtain the maximum allowed permissions for a token.
        ''' </summary>
        MaximumAllowed = &H2000000

        ''' <summary>
        ''' Grants the right to create a new console for the token.
        ''' </summary>
        CreateNewConsole = &H10

        ''' <summary>
        ''' Sets the process priority class to normal.
        ''' </summary>
        NormalPriorityClass = &H20

        ''' <summary>
        ''' Indicates that the process should inherit standard handles (stdin, stdout, stderr).
        ''' </summary>
        StartFUseStdHandles = &H100
    End Enum
End Namespace

Namespace CoreServices.WindowsApiInterop.Enums

    ''' <summary>
    ''' Specifies the type of token, distinguishing between a primary token and an impersonation token.
    ''' </summary>
    ''' <remarks>
    ''' The <see cref="TokenType"/> enum includes values that indicate whether a token is a primary token or an impersonation token.
    ''' For detailed information about token types, refer to:
    ''' <see href="https://msdn.microsoft.com/en-us/data/aa379633(v=vs.71)">TOKEN_TYPE Enumeration</see>.
    ''' 
    ''' This enum corresponds to the following C++ definition:
    ''' <c>typedef enum _TOKEN_TYPE {
    '''   TokenPrimary = 1,
    '''   TokenImpersonation
    ''' } TOKEN_TYPE;</c>
    ''' 
    ''' The <c>&lt;UsedImplicitly&gt;</c> attribute is applied to the <see cref="TokenImpersonation"/> member to suppress warnings about it being unused.
    ''' This ensures that the enum is kept complete for completeness and future extensibility, despite not being utilized directly in the current project.
    ''' </remarks>
    Friend Enum TokenType As Integer

        ''' <summary>
        ''' Indicates that the token is a primary token.
        ''' </summary>
        TokenPrimary = 1

        ''' <summary>
        ''' Indicates that the token is an impersonation token.
        ''' </summary>
        <UsedImplicitly>
        TokenImpersonation = 2
    End Enum
End Namespace

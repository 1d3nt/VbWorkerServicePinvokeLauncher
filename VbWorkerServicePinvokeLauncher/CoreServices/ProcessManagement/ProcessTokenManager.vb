Namespace CoreServices.ProcessManagement

Public Class ProcessTokenManager

    ''' <summary>
    '''     Using the OpenProcessToken function to retrieve a handle to the primary token of a process.
    ''' </summary>
    ''' <param name="processHandle">
    '''     The unique handle for the associated process.
    ''' </param>
    ''' <param name="tokenHandle">
    '''     A pointer to a handle that identifies the newly opened access token when the function returns.
    ''' </param>
    ''' <returns>
    '''     The access token associated with the process returned from <see cref="GetSpecifiedId" /> method.
    ''' </returns>
    Public Shared Function TryOpenProcessToken(processHandle As IntPtr, ByRef tokenHandle As IntPtr) As Boolean
        Return Methods.NativeMethods.OpenProcessToken(processHandle, AccessMask.TokenDuplicate, tokenHandle)
    End Function

    ''' <summary>
    '''     The OpenProcessToken function opens the access token associated with a process.
    ''' </summary>
    ''' <param name="attributes">
    '''     The security attributes returned from <see cref="GetSecurityAttributes" /> method.
    ''' </param>
    ''' <param name="tokenHandle">
    '''     The access token associated with the process.
    ''' </param>
    ''' <param name="hToken">
    '''     A pointer to a variable that receives a handle to the duplicate token.
    ''' </param>
    ''' <returns>
    '''     The duplicate access token associated with the process returned from <see cref="TryOpenProcessToken" /> method.
    ''' </returns>
    Public Shared Function TryDuplicateToken(ByRef attributes As SecurityAttributes, tokenHandle As IntPtr, ByRef hToken As IntPtr) As Boolean
        Return Methods.NativeMethods.DuplicateTokenEx(tokenHandle, AccessMask.MaximumAllowed, attributes, SecurityImpersonationLevel.SecurityIdentification, TokenType.TokenPrimary, hToken)
    End Function
End Class
End Namespace

Namespace CoreServices.ProcessManagement

    ''' <summary>
    ''' Provides methods for managing access tokens associated with processes.
    ''' </summary>
    ''' <remarks>
    ''' The <see cref="ProcessTokenManager"/> class contains methods for opening process tokens and duplicating them. 
    ''' This is useful for scenarios where you need to run processes with elevated privileges or under different security contexts.
    ''' </remarks>
    Friend Class ProcessTokenManager

        ''' <summary>
        ''' Using the <see cref="Methods.NativeMethods.OpenProcessToken"/> function to retrieve a handle to the primary token of a process.
        ''' </summary>
        ''' <param name="processHandle">
        ''' The unique handle for the associated process.
        ''' </param>
        ''' <param name="tokenHandle">
        ''' A pointer to a handle that identifies the newly opened access token when the function returns.
        ''' </param>
        ''' <returns>
        ''' <c>True</c> if the function succeeds; otherwise, <c>false</c>. The access token associated with the process is returned in <paramref name="tokenHandle"/>.
        ''' </returns>
        ''' <remarks>
        ''' This method uses the <see cref="Methods.NativeMethods.OpenProcessToken"/> function to obtain a handle to the primary token of the specified process.
        ''' The token handle is then used for further operations, such as duplicating the token or creating processes with the token's security context.
        ''' </remarks>
        ''' <remarks>
        ''' This method is marked with a suppression attribute because ReSharper may incorrectly suggest 
        ''' that the method should be marked as <c>Shared</c> (static). However, the method is not <c>Shared</c> 
        ''' because it relies on instance-level operations and creating new instances of the class.
        ''' Making it <c>Shared</c> would prevent instance creation and break the intended functionality.
        ''' </remarks>
        <SuppressMessage("StaticMembers", "CA1822:Mark members as static", Justification:="ReSharper incorrectly suggests marking this method as Shared. The method relies on instance-level operations.")>
        Friend Function TryOpenProcessToken(processHandle As IntPtr, ByRef tokenHandle As IntPtr) As Boolean
            Return Methods.NativeMethods.OpenProcessToken(processHandle, AccessMask.TokenDuplicate, tokenHandle)
        End Function

        ''' <summary>
        ''' Uses the <see cref="Methods.NativeMethods.DuplicateTokenEx"/> function to duplicate the access token associated with a process.
        ''' </summary>
        ''' <param name="attributes">
        ''' The security attributes returned from the <see cref="GetSecurityAttributes"/> method.
        ''' </param>
        ''' <param name="tokenHandle">
        ''' The access token associated with the process.
        ''' </param>
        ''' <param name="hToken">
        ''' A pointer to a variable that receives a handle to the duplicate token.
        ''' </param>
        ''' <returns>
        ''' <c>True</c> if the function succeeds; otherwise, <c>false</c>. The duplicate access token is returned in <paramref name="hToken"/>.
        ''' </returns>
        ''' <remarks>
        ''' This method uses the <see cref="Methods.NativeMethods.DuplicateTokenEx"/> function to duplicate the access token.
        ''' The duplicated token allows for creating processes in the security context represented by the token.
        ''' For more details on the structure and its fields, refer to the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-duplicatetokenex">DuplicateTokenEx documentation</see>.
        ''' </remarks>
        ''' <remarks>
        ''' This method is marked with a suppression attribute because ReSharper may incorrectly suggest 
        ''' that the method should be marked as <c>Shared</c> (static). However, the method is not <c>Shared</c> 
        ''' because it relies on instance-level operations and creating new instances of the class.
        ''' Making it <c>Shared</c> would prevent instance creation and break the intended functionality.
        ''' </remarks>
        <SuppressMessage("StaticMembers", "CA1822:Mark members as static", Justification:="ReSharper incorrectly suggests marking this method as Shared. The method relies on instance-level operations.")>
        Friend Function TryDuplicateToken(ByRef attributes As SecurityAttributes, tokenHandle As IntPtr, ByRef hToken As IntPtr) As Boolean
            Return Methods.NativeMethods.DuplicateTokenEx(tokenHandle, AccessMask.MaximumAllowed, attributes, SecurityImpersonationLevel.SecurityIdentification, TokenType.TokenPrimary, hToken)
        End Function

        ''' <summary>
        ''' Creates and initializes a <see cref="SecurityAttributes"/> structure that contains the security information for a securable object.
        ''' </summary>
        ''' <returns>
        ''' A <see cref="SecurityAttributes"/> structure that specifies the security attributes for the object. The structure is configured with its length and inheritance properties.
        ''' </returns>
        ''' <remarks>
        ''' The <see cref="SecurityAttributes"/> structure is used by functions like <see cref="Methods.NativeMethods.CreateProcessAsUser"/> to define the security characteristics of a process or thread. 
        ''' For more details on the structure and its fields, refer to the <see href="https://learn.microsoft.com/en-us/windows/win32/api/wtypesbase/ns-wtypesbase-security_attributes">SecurityAttributes documentation</see>.
        ''' </remarks>
        ''' <remarks>
        ''' This method is marked with a suppression attribute because ReSharper may incorrectly suggest 
        ''' that the method should be marked as <c>Shared</c> (static). However, the method is not <c>Shared</c> 
        ''' because it relies on instance-level operations and creating new instances of the class.
        ''' Making it <c>Shared</c> would prevent instance creation and break the intended functionality.
        ''' </remarks>
        <SuppressMessage("StaticMembers", "CA1822:Mark members as static", Justification:="ReSharper incorrectly suggests marking this method as Shared. The method relies on instance-level operations.")>
        Friend Function GetSecurityAttributes() As SecurityAttributes
            Dim attributes As New SecurityAttributes() With {
                        .nLength = Marshal.SizeOf(attributes),
                        .bInheritHandle = True
                    }
            Return attributes
        End Function
    End Class
End Namespace

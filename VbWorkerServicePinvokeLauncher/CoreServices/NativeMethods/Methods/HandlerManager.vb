namespace CoreServices.NativeMethods.Methods

    ''' <summary>
    ''' Provides utility methods for managing handles in P/Invoke operations.
    ''' </summary>
    ''' <remarks>
    ''' This class contains methods to handle common operations related to handle management,
    ''' such as closing handles if they are not null. It is marked as <c>NotInheritable</c> to
    ''' prevent inheritance and ensure that the utility methods are used as intended.
    ''' </remarks>
    Friend NotInheritable Class HandleManager

        ''' <summary>
        ''' Closes the token handle if it is not null.
        ''' </summary>
        ''' <param name="tokenHandle">The handle to be closed.</param>
        Friend Shared Sub CloseTokenHandleIfNotNull(tokenHandle As IntPtr)
            If Not tokenHandle.Equals(NativeMethods.NullHandleValue) Then
                NativeMethods.CloseHandle(tokenHandle)
            End If
        End Sub
    End Class
End namespace

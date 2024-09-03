Namespace CoreServices.WindowsApiInterop.Methods

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
        ''' <remarks>
        ''' This method checks if the provided <paramref name="tokenHandle"/> is not equal to <see cref="NativeMethods.NullHandleValue"/>. 
        ''' If it is not equal, the method proceeds to close the handle by calling the <see cref="NativeMethods.CloseHandle"/> method. 
        ''' If the handle is equal to <see cref="NativeMethods.NullHandleValue"/>, which indicates an invalid or uninitialized handle, 
        ''' the method skips the closing operation.
        ''' </remarks>
        Friend Shared Sub CloseTokenHandleIfNotNull(tokenHandle As IntPtr)
            If Not Equals(tokenHandle, NativeMethods.NullHandleValue) Then
                NativeMethods.CloseHandle(tokenHandle)
            End If
        End Sub
    End Class
End Namespace

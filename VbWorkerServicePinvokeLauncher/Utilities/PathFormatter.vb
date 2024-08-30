Namespace Utilities

    ''' <summary>
    ''' Provides methods for formatting paths.
    ''' </summary>
    ''' <remarks>
    ''' The <see cref="PathFormatter"/> class contains utilities for manipulating and standardizing file paths. 
    ''' In particular, it helps in converting path separators to ensure compatibility with Windows-style paths.
    ''' </remarks>
    Friend Class PathFormatter

        ''' <summary>
        ''' Formats forward slashes in a path to use backslashes.
        ''' </summary>
        ''' <param name="value">
        ''' The path to be formatted. This path may contain forward slashes ('/') that need to be replaced with backslashes ('\') to avoid command line option specifier conflicts.
        ''' </param>
        ''' <returns>
        ''' A correctly formatted path where all forward slashes have been replaced with backslashes.
        ''' </returns>
        ''' <remarks>
        ''' This method is useful for standardizing file paths on Windows systems, where backslashes are used as path separators. 
        ''' This transformation helps in ensuring compatibility with APIs or command-line tools that expect Windows-style paths.
        ''' </remarks>
        Friend Shared Function CreateRelativePath(value As String) As String
            Return Text.RegularExpressions.Regex.Replace(value, "/", "\")
        End Function
    End Class
End Namespace

Namespace Utilities

    ''' <summary>
    ''' Provides file validation functionality for the worker service.
    ''' </summary>
    ''' <remarks>
    ''' This class contains methods for validating file paths to ensure that they are both non-empty and that the specified file exists.
    ''' </remarks>
    Friend Class FileValidator

        ''' <summary>
        ''' Checks if the file path is valid and the file exists.
        ''' </summary>
        ''' <param name="filePath">
        ''' The file path to check. This parameter should be a string representing the path to the file.
        ''' </param>
        ''' <returns>
        ''' True if the file path is valid and the file exists; otherwise, false.
        ''' </returns>
        ''' <remarks>
        ''' This method verifies whether the provided file path is not null or empty and if the file at the given path exists on the filesystem.
        ''' </remarks>
        ''' <exception cref="System.ArgumentNullException">
        ''' Thrown when <paramref name="filePath"/> is <c>Nothing</c>.
        ''' </exception>
        <SuppressMessage("Design", "CA1054:URI parameters should not be strings", Justification:="File paths are inherently strings, and the method operates within a file system context where this is the expected parameter type.")>
        Public Function IsValidFilePath(filePath As String) As Boolean
            Return Not String.IsNullOrEmpty(filePath) AndAlso File.Exists(filePath)
        End Function
    End Class
End Namespace

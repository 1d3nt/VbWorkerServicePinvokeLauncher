Namespace CoreServices.ProcessManagement

    ''' <summary>
    ''' Provides process starting functionality for the worker service.
    ''' </summary>
    Friend Class ProcessStarter

        ''' <summary>
        ''' An instance of <see cref="ILogger{Worker}"/> used for logging messages.
        ''' </summary>
        Private ReadOnly _logger As ILogger(Of Worker)

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ProcessStarter"/> class.
        ''' </summary>
        ''' <param name="logger">An instance of <see cref="ILogger{Worker}"/> used for logging messages.</param>
        Friend Sub New(logger As ILogger(Of Worker))
            _logger = logger
        End Sub

        ''' <summary>
        ''' Attempts to start the process using the specified file path.
        ''' </summary>
        ''' <param name="filePath">The file path of the executable to start.</param>
        Friend Sub StartProcess(filePath As String)
            Try
                Dim tokenManager As New ProcessTokenManager()
                Dim processInfoRetriever As New ProcessInfoRetriever()
                Dim processLauncher = New ElevatedProcessLauncher(tokenManager, processInfoRetriever)
                processLauncher.TryCreateProcess(filePath)
            Catch ex As Exception
                _logger.LogError(ex, "An error occurred while starting the executable.")
            End Try
        End Sub
    End Class
End Namespace

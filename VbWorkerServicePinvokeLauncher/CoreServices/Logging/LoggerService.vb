Namespace CoreServices.Logging

    ''' <summary>
    ''' Provides logging functionality for the worker service.
    ''' </summary>
    Friend Class LoggerService

        ''' <summary>
        ''' An instance of <see cref="ILogger{Worker}"/> used for logging messages.
        ''' </summary>
        Private ReadOnly _logger As ILogger(Of Worker)

        ''' <summary>
        ''' Initializes a new instance of the <see cref="LoggerService"/> class.
        ''' </summary>
        ''' <param name="logger">An instance of <see cref="ILogger{Worker}"/> used for logging messages.</param>
        Friend Sub New(logger As ILogger(Of Worker))
            _logger = logger
        End Sub

        ''' <summary>
        ''' Logs the current time when the worker is running.
        ''' </summary>
        Friend Sub LogWorkerRunning()
            If _logger.IsEnabled(LogLevel.Information) Then
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now)
            End If
        End Sub
        ''' <summary>
        ''' Logs a warning message.
        ''' </summary>
        ''' <param name="message">The warning message to log.</param>
        Friend Sub LogWarning(message As String)
            _logger.LogWarning("Warning: {message}", message)
        End Sub
    End Class
End Namespace

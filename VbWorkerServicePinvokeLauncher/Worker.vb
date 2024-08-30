Public Class Worker
    Inherits BackgroundService

    ''' <summary>
    '''     The default instance to run as.
    ''' </summary>
    Private Const DefaultRunAs As String = "winlogon"
    Private ReadOnly _logger As ILogger(Of Worker)
    Private ReadOnly _filePath As String
    Private _processStarted As Boolean = False

    Public Sub New(logger As ILogger(Of Worker), configuration As IConfiguration)
        _logger = logger
        _filePath = configuration.GetValue(Of String)("WorkerServiceSettings:FilePath")
    End Sub

    Protected Overrides Async Function ExecuteAsync(stoppingToken As CancellationToken) As Task
        Do While Not stoppingToken.IsCancellationRequested
            If Not _processStarted Then
                If _logger.IsEnabled(LogLevel.Information) Then
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now)
                End If
                If Not String.IsNullOrEmpty(_filePath) AndAlso File.Exists(_filePath) Then
                    Try
                        Dim processLauncher = New ElevatedProcessLauncher
                        processLauncher.TryCreateProcess(_filePath, DefaultRunAs)
                        _processStarted = True 
                    Catch ex As Exception
                        _logger.LogError(ex, "An error occurred while starting the executable.")
                    End Try
                Else
                    _logger.LogWarning("File path is invalid or file does not exist.")
                End If
            End If
            Await Task.Delay(100000, stoppingToken)
        Loop
    End Function
End Class

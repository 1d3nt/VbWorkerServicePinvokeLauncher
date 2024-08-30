''' <summary>
''' Represents a background service that periodically checks and starts a process using elevated privileges.
''' </summary>
Public Class Worker
    Inherits BackgroundService

    ''' <summary>
    ''' An instance of <see cref="ILogger{Worker}"/> used for logging messages.
    ''' </summary>
    Private ReadOnly _logger As ILogger(Of Worker)

    ''' <summary>
    ''' The file path to the executable that the worker service will manage.
    ''' </summary>
    ''' <remarks>
    ''' This path is retrieved from the configuration settings and used to determine which file to launch.
    ''' </remarks>
    Private ReadOnly _filePath As String

    ''' <summary>
    ''' A flag indicating whether the process has been started.
    ''' </summary>
    ''' <remarks>
    ''' This boolean value is used to ensure that the process is only started once. It is initially set to <c>False</c>
    ''' and is set to <c>True</c> once the process has been successfully started.
    ''' </remarks>
    Private _processStarted As Boolean = False

    ''' <summary>
    ''' Initializes a new instance of the <see cref="Worker"/> class.
    ''' </summary>
    ''' <param name="logger">
    ''' An instance of <see cref="ILogger{Worker}"/> used for logging messages.
    ''' </param>
    ''' <param name="configuration">
    ''' An instance of <see cref="IConfiguration"/> used to retrieve configuration settings.
    ''' </param>
    ''' <remarks>
    ''' The constructor initializes the logger and retrieves the file path from the configuration settings.
    ''' </remarks>
    Public Sub New(logger As ILogger(Of Worker), configuration As IConfiguration)
        _logger = logger
        _filePath = configuration.GetValue(Of String)("WorkerServiceSettings:FilePath")
    End Sub

    ''' <summary>
    ''' Executes the background service asynchronously.
    ''' </summary>
    ''' <param name="stoppingToken">
    ''' A <see cref="CancellationToken"/> that can be used to cancel the operation.
    ''' </param>
    ''' <returns>
    ''' A <see cref="Task"/> that represents the asynchronous operation.
    ''' </returns>
    ''' <remarks>
    ''' This method periodically checks if the process has already been started. If not, it logs the current time, checks if the
    ''' specified file exists, and attempts to start the process using <see cref="ElevatedProcessLauncher"/>. It also handles any
    ''' exceptions that occur during the process start-up.
    ''' </remarks>
    Protected Overrides Async Function ExecuteAsync(stoppingToken As CancellationToken) As Task
        Do While Not stoppingToken.IsCancellationRequested
            If Not _processStarted Then
                LogWorkerRunning()
                If IsValidFilePath(_filePath) Then
                    StartProcess(_filePath)
                Else
                    _logger.LogWarning("File path is invalid or file does not exist.")
                End If
            End If
            Await Task.Delay(100000, stoppingToken)
        Loop
    End Function

    ''' <summary>
    ''' Logs the current time when the worker is running.
    ''' </summary>
    Private Sub LogWorkerRunning()
        If _logger.IsEnabled(LogLevel.Information) Then
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now)
        End If
    End Sub

    ''' <summary>
    ''' Checks if the file path is valid and the file exists.
    ''' </summary>
    ''' <param name="filePath">The file path to check.</param>
    ''' <returns>True if the file path is valid and the file exists; otherwise, false.</returns>
    Private Shared Function IsValidFilePath(filePath As String) As Boolean
        Return Not String.IsNullOrEmpty(filePath) AndAlso File.Exists(filePath)
    End Function

    ''' <summary>
    ''' Attempts to start the process using the specified file path.
    ''' </summary>
    ''' <param name="filePath">The file path of the executable to start.</param>
    ''' <remarks>
    ''' Instances of <see cref="ProcessTokenManager"/> and <see cref="ProcessInfoRetriever"/> are created within this method
    ''' to adhere to the Single Responsibility Principle (SRP). This ensures that the <see cref="ElevatedProcessLauncher"/>
    ''' class is only responsible for launching processes and not for managing its dependencies.
    ''' </remarks>
    Private Sub StartProcess(filePath As String)
        Try
            Dim tokenManager As New ProcessTokenManager()
            Dim processInfoRetriever As New ProcessInfoRetriever()
            Dim processLauncher = New ElevatedProcessLauncher(tokenManager, processInfoRetriever)
            processLauncher.TryCreateProcess(filePath)
            _processStarted = True
        Catch ex As Exception
            _logger.LogError(ex, "An error occurred while starting the executable.")
        End Try
    End Sub
End Class

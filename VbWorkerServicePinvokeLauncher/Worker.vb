Imports VbWorkerServicePinvokeLauncher.Utilities
Imports VbWorkerServicePinvokeLauncher.CoreServices.Logging

''' <summary>
''' Represents a background service that periodically checks and starts a process using elevated privileges.
''' </summary>
Public Class Worker
    Inherits BackgroundService

    ''' <summary>
    ''' Provides logging functionality for the worker service.
    ''' </summary>
    Private ReadOnly _loggerService As LoggerService

    ''' <summary>
    ''' Provides file validation functionality for the worker service.
    ''' </summary>
    Private ReadOnly _fileValidator As FileValidator

    ''' <summary>
    ''' Provides process starting functionality for the worker service.
    ''' </summary>
    Private ReadOnly _processStarter As ProcessStarter

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
    ''' <param name="logger">An instance of <see cref="ILogger{Worker}"/> used for logging messages.</param>
    ''' <param name="configuration">An instance of <see cref="IConfiguration"/> used to retrieve configuration settings.</param>
    ''' <remarks>
    ''' The constructor initializes the logger, file validator, and process starter services. 
    ''' This ensures that all dependencies are properly configured and available for use when the worker service starts.
    ''' By injecting these dependencies, the class adheres to the Single Responsibility Principle (SRP),
    ''' as each service is responsible for a specific aspect of the functionality, making the code more modular and maintainable.
    ''' </remarks>
    Public Sub New(logger As ILogger(Of Worker), configuration As IConfiguration)
        _loggerService = New LoggerService(logger)
        _fileValidator = New FileValidator()
        _processStarter = New ProcessStarter(logger)
        _filePath = configuration.GetValue(Of String)("WorkerServiceSettings:FilePath")
    End Sub

    ''' <summary>
    ''' Executes the background service asynchronously.
    ''' </summary>
    ''' <param name="stoppingToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    ''' <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    Protected Overrides Async Function ExecuteAsync(stoppingToken As CancellationToken) As Task
        Do While Not stoppingToken.IsCancellationRequested
            If Not _processStarted Then
                _loggerService.LogWorkerRunning()
                If _fileValidator.IsValidFilePath(_filePath) Then
                    _processStarter.StartProcess(_filePath)
                    _processStarted = True
                Else
                    _loggerService.LogWarning("File path is invalid or file does not exist.")
                End If
            End If
            Await Task.Delay(100, stoppingToken)
        Loop
    End Function
End Class

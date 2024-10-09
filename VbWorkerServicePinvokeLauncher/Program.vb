''' <author>
''' Sam (ident)
''' Twitter: <see href="https://twitter.com/1d3nt">https://twitter.com/1d3nt</see>
''' GitHub: <see href="https://github.com/1d3nt">https://github.com/1d3nt</see>
''' Email: <see href="mailto:ident@simplecoders.com">ident@simplecoders.com</see>
''' VBForums: <see href="https://www.vbforums.com/member.php?113656-ident">https://www.vbforums.com/member.php?113656-ident</see>
''' ORCID: <see href="https://orcid.org/0009-0007-1363-3308">https://orcid.org/0009-0007-1363-3308</see>
''' </author>
''' <date>07/09/2024</date>
''' <version>1.0.0</version>
''' <license>Creative Commons Attribution 4.0 International (CC BY 4.0) - See LICENSE.md for details</license>
''' <summary>
''' The entry point for the application.
''' </summary>
''' <remarks>
''' This module contains the <see cref="Main"/> method, which sets up and starts the application host.
''' 
''' This project includes P/Invoke declarations and methods for interacting with Windows API functions.
''' Contributions and enhancements are welcomed to further extend the functionality and improve the integration.
''' 
''' Just a hobby programmer that enjoys P/Invoke and exploring complex interactions with system-level APIs.
''' My mallory x
''' </remarks>
Module Program

    ''' <summary>
    ''' The main entry point for the VbWorkerServicePinvokeLauncher application.
    ''' This method configures and initializes the .NET Generic Host for the Worker Service.
    ''' </summary>
    ''' <param name="args">Command-line arguments passed to the application at startup.</param>
    ''' <remarks>
    ''' This method sets up the application builder with a JSON configuration file (`appsettings.json`) 
    ''' to manage settings for the service. It registers the service with Windows using the <c>WindowsService</c> 
    ''' options, which allow the application to run as a background service.
    ''' 
    ''' The service's primary functionality includes launching processes under specific user accounts, 
    ''' including the SYSTEM account, by duplicating process tokens to manage elevated privileges. 
    ''' This is achieved through P/Invoke calls, allowing interaction with low-level Windows API functions.
    ''' 
    ''' The <see cref="Worker"/> service is registered as a hosted service, and once the host is built, 
    ''' the service starts running using <c>Host.Run</c>. This ensures the application operates 
    ''' in the background, providing essential process management capabilities with elevated privileges.
    ''' 
    ''' Features:
    ''' <list type="bullet">
    ''' <item>Launch processes under specific user accounts, including SYSTEM.</item>
    ''' <item>Duplicate process tokens for elevated privilege management.</item>
    ''' <item>Use P/Invoke to interact with Windows APIs.</item>
    ''' <item>Configuration managed via `appsettings.json`.</item>
    ''' </list>
    ''' </remarks>
    Sub Main(args As String())
        Dim builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder(args)
        builder.Configuration.AddJsonFile("appsettings.json", optional:=True, reloadOnChange:=True)
        builder.Services.AddWindowsService(Sub(options)
                                               options.ServiceName = "VbWorkerService"
                                           End Sub)
        builder.Services.AddHostedService(Of Worker)()
        Dim host = builder.Build()
        host.Run()
    End Sub
End Module

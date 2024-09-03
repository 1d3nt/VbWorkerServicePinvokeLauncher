''' <author>
''' Sam (ident)
''' Twitter: <see href="https://twitter.com/1d3nt">https://twitter.com/1d3nt</see>
''' GitHub: <see href="https://github.com/1d3nt">https://github.com/1d3nt</see>
''' Email: <see href="mailto:ident@simplecoders.com">ident@simplecoders.com</see>
''' VBForums: <see href="https://www.vbforums.com/member.php?113656-ident">https://www.vbforums.com/member.php?113656-ident</see>
''' </author>
''' <date>31/08/2024</date>
''' <summary>
''' The entry point for the application.
''' </summary>
''' <remarks>
''' This module contains the <see cref="Main"/> method, which sets up and starts the application host.
''' 
''' This project includes P/Invoke declarations and methods for interacting with Windows API functions.
''' Contributions and enhancements are welcomed to further extend the functionality and improve the integration.
''' 
''' Just a hobby programmer that enjoys P/Invoke.
''' </remarks>
Module Program

    ''' <summary>
    ''' The main entry point for the application.
    ''' </summary>
    ''' <param name="args">
    ''' An array of <see cref="String"/> arguments passed to the application at startup.
    ''' </param>
    ''' <remarks>
    ''' This method configures the application builder, sets up configuration files, registers the Windows service options,
    ''' and adds the <see cref="Worker"/> as a hosted service. After building the host, it starts the application.
    ''' 
    ''' The method is designed to be the main entry point for console applications that require 
    ''' integration with Windows services. It leverages the .NET Generic Host to facilitate 
    ''' the management of background services and configuration.
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

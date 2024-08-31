''' Author: Sam (ident)
''' Twitter: https://twitter.com/1d3nt
''' GitHub: https://github.com/1d3nt
''' Email: ident@simplecoders.com
''' VbForums: https://www.vbforums.com/member.php?113656-ident
''' Date: 31/08/2024
''' 
''' Just a hobby programmer that enjoys P/Invoke.
''' <summary>
''' The entry point for the application.
''' </summary>
''' <remarks>
''' This module contains the <see cref="Main"/> method, which sets up and starts the application host.
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

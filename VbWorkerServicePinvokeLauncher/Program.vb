''' <author>
''' Sam (ident)
''' Twitter: <see href="https://twitter.com/1d3nt">https://twitter.com/1d3nt</see>
''' GitHub: <see href="https://github.com/1d3nt">https://github.com/1d3nt</see>
''' Email: <see href="mailto:ident@simplecoders.com">ident@simplecoders.com</see>
''' VBForums: <see href="https://www.vbforums.com/member.php?113656-ident">https://www.vbforums.com/member.php?113656-ident</see>
''' ORCID: <see href="https://orcid.org/0009-0007-1363-3308">https://orcid.org/0009-0007-1363-3308</see>
''' </author>
''' <date>31/08/2024</date>
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
    ''' Entry point for the console application. This application manages user interactions to configure and set up services. 
    ''' It prompts users for input to determine whether to proceed with various setup tasks and performs actions based on their responses.
    ''' </summary>
    ''' <param name="args">Command-line arguments (not used in this implementation).</param>
    ''' <remarks>
    ''' The <paramref name="args"/> parameter is included to adhere to the standard signature for a console application's
    ''' <see cref="Main"/> method. While this parameter is not utilized in the current implementation, including it follows 
    ''' best practices and ensures consistency with the conventional entry point signature.
    ''' 
    ''' This method configures the application builder, sets up configuration files, registers the Windows service options,
    ''' and adds the <see cref="Worker"/> as a hosted service. After building the host, it starts the application.
    ''' 
    ''' The method is designed to be the main entry point for console applications that require 
    ''' integration with Windows services. It leverages the .NET Generic Host to facilitate 
    ''' the management of background services and configuration.
    ''' </remarks>
    <SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification:="Standard Main method parameter signature.")>
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

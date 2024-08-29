Module Program
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

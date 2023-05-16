using Microsoft.Extensions.Hosting;


using var host = Host.CreateDefaultBuilder(args).ConfigureServices(
    services =>
    {
        services.AddImportLogic();
    })
    .Build();

await host.StartAsync();
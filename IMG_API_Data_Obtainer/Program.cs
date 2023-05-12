﻿using Microsoft.Extensions.Hosting;


using var host = Host.CreateDefaultBuilder(args).ConfigureServices(
    services =>
    {
        services.AddLogic();
    })
    .Build();

await host.StartAsync();
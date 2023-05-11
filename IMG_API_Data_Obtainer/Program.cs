using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IMG_API_Data_Obtainer.Logic;
using IMG_API_Data_Obtainer.Logic.Abstractions;
using IMG_API_Data_Obtainer.Services;
using ADF.Library.Models.Mapping;
using ADF.Library.Models.Mapping.Feed.Description;
using ADF.Library.Models.Mapping.Internal.Description;

using var host = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    services.AddLogic();
})
.Build();

await host.StartAsync();
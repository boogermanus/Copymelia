// See https://aka.ms/new-console-template for more information

using Copymelia.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

var host = Host.CreateDefaultBuilder(args);
host.ConfigureServices(services =>
{
    services.AddLogging(builder => { builder.AddConsole();});
    services.AddSingleton<App>();
});
host.ConfigureAppConfiguration(builder =>
{
    builder.AddCommandLine(args);
});

var build = host.Build();
var app = build.Services.GetService<App>();
app?.Run();



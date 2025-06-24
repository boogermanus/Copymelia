// See https://aka.ms/new-console-template for more information

using Copymelia.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

var host = Host.CreateDefaultBuilder(args);
host.ConfigureServices(services =>
{
    services.AddLogging(builder => { builder.AddConsole();});
    services.AddHostedService<App>();
});

var app = host.Build();
app.Run();


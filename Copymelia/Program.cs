// See https://aka.ms/new-console-template for more information

using Copymelia.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

var host = Host.CreateDefaultBuilder(args);
host.ConfigureServices(services =>
{
    services.AddLogging(builder => { builder.AddConsole();});
    services.AddSingleton<App>();
});

var build = host.Build();
var app = build.Services.GetService<App>();
app?.Run();



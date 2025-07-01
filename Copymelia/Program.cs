// See https://aka.ms/new-console-template for more information

using Copymelia.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

var host = Host.CreateApplicationBuilder();
host.Services.AddLogging(builder => {builder.AddConsole();});
host.Services.AddSingleton<App>();

host.Configuration.AddCommandLine(args);
host.Configuration.AddJsonFile("appsettings.json");

var build = host.Build();
var app = build.Services.GetService<App>();
app?.Run(args);



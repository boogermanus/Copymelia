// See https://aka.ms/new-console-template for more information

using Copymelia;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = Host.CreateApplicationBuilder();
host.Logging.AddConsole();
host.Services.AddSingleton<App>();

var build = host.Build();
var app = build.Services.GetService<App>();
app?.Run(args);
Console.ReadLine();

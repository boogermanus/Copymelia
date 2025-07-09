// See https://aka.ms/new-console-template for more information

using Copymelia.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

var host = Host.CreateApplicationBuilder();

host.Logging.AddConsole();

host.Services.AddSingleton<App>();
host.Services.AddSingleton<FileProcessor>();
host.Services.AddSingleton<OutputDirector>();
host.Services.AddSingleton<MoveDirector>();

host.Configuration.AddCommandLine(args);
host.Configuration.AddJsonFile("appsettings.json");

var build = host.Build();
var app = build.Services.GetService<App>();

app?.Run(args);

// so dumb, but you have to do this in order to flush the logs
Console.Out.Flush();
Console.WriteLine("Press any key to exit");
Console.ReadLine();



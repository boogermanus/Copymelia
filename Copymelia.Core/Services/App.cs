using System.Net.Security;
using CommandLine;
using Copymelia.Core.Models;
using Microsoft.Extensions.Logging;

namespace Copymelia.Core.Services;

public class App 
{
    private readonly ILogger _logger;
    private Options _options;
    public App(ILogger<App> logger)
    {
        _logger = logger;
    }

    public void Run(string[] args)
    {
        ParseArguments(args);
    }

    private void ParseArguments(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(HandleParse)
            .WithNotParsed(HandleParseError);
    }

    private void HandleParse(Options options)
    {
        if(!Directory.Exists(options.Path))
            _logger.LogError($"Path '{options.Path}' does not exist");
    }
    private void HandleParseError(IEnumerable<Error> errors)
    {
        errors.ToList().ForEach(e => _logger.LogDebug(e.ToString()));
    }
}
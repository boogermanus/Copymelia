using CommandLine;
using Copymelia.Models;
using Microsoft.Extensions.Logging;

namespace Copymelia.Services;

public class App 
{
    private readonly ILogger _logger;
    private Options _options;
    private FileProcessor _fileProcessor;
    public App(ILogger<App> logger, FileProcessor processor)
    {
        _logger = logger;
        _options = new Options();
        _fileProcessor = processor;
    }

    public void Run(string[] args)
    {
        ParseArguments(args);
        _fileProcessor.Process(_options);
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
        
        if(!Directory.Exists(options.Output))
            _logger.LogError($"Output directory '{options.Output}' does not exist");
        
        if(options.WhatIf)
            _logger.LogInformation("WhatIf is enabled");
        
        _options = options;
    }
    private void HandleParseError(IEnumerable<Error> errors)
    {
        errors.ToList().ForEach(e => _logger.LogDebug(e.ToString()));
    }
}
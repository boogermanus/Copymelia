using CommandLine;
using Copymelia.Core.Constants;
using Copymelia.Models;
using Microsoft.Extensions.Logging;

namespace Copymelia.Services;

public class App 
{
    private readonly ILogger _logger;
    private Options _options;
    private readonly FileProcessor _fileProcessor;
    private readonly OutputDirector _outputDirector;
    private bool _canRun;
    public App(ILogger<App> logger, FileProcessor processor, OutputDirector director)
    {
        _logger = logger;
        _options = new Options();
        _fileProcessor = processor;
        _outputDirector = director;
    }

    public void Run(string[] args)
    {
        ParseArguments(args);
        
        if (!_canRun) return;
        
        _outputDirector.Build(_options);
        _fileProcessor.Process(_options);
    }

    private void ParseArguments(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(HandleParse);

    }

    private void HandleParse(Options options)
    {
        if (!Directory.Exists(options.Path))
        {
            _logger.LogError($"Path '{options.Path}' does not exist");
            return;
        }

        if(options.WhatIf)
            _logger.LogInformation("WhatIf is enabled");

        if (options.Mode != Modes.Move && options.Mode != Modes.Copy)
        {
            _logger.LogError($"Mode '{options.Mode}' is not a valid mode'");
            return;
        }
        
        _options = options;
        _canRun = true;
    }
}
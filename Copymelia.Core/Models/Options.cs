using CommandLine;

namespace Copymelia.Core.Models;

public class Options
{
    [Option(Required = true, HelpText = "Path of directory to scan.")]
    public string Path { get; set; } = string.Empty;
    
    [Option(Required = true, HelpText = @"Path of directory to move files to. 
Note: if --path contains spaces omit the trailing slash")]
    public string Output { get; set; } = string.Empty;
    
    [Option(Default = false, Required = false, HelpText = "Scan but do not operate on results.")]
    public bool WhatIf { get; set; }

    [Option(Default = "move", Required = false, HelpText = "Mode [move|copy].")]
    public string Mode { get; set; } = "move";
}
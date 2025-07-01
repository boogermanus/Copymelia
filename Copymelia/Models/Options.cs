using CommandLine;

namespace Copymelia.Models;

public class Options
{
    [Option(Required = true, HelpText = "Path to directory to scan")]
    public string Path { get; set; } = string.Empty;
    
    [Option(Required = true, HelpText = "Path to the directory to copy to")]
    public string Output { get; set; } = string.Empty;
    
    [Option(Default = false, Required = false, HelpText = "Scan but do not operate on results")]
    public bool WhatIf { get; set; }
}
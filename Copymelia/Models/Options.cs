using CommandLine;

namespace Copymelia.Models;

public class Options
{
    [Option(Required = true, HelpText = "Path of directory to scan")]
    public string Path { get; set; } = string.Empty;
    
    [Option(Required = true, HelpText = "Path of directory to move files to")]
    public string Output { get; set; } = string.Empty;
    
    [Option(Default = false, Required = false, HelpText = "Scan but do not operate on results")]
    public bool WhatIf { get; set; }
}
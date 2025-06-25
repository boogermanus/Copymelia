using CommandLine;

namespace Copymelia.Core.Models;

public class Options
{
    [Option(Required = true, HelpText = "Path to directory to scan")]
    public string Path { get; set; }
    [Option(Default = false, Required = false, HelpText = "Scan but do not operate on results")]
    public bool WhatIf { get; set; }
}
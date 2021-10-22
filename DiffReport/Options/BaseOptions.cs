// <copyright file="BaseOptions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace DiffReport.Options
{
    public class BaseOptions
    {
        [Option("outputDir", Required = false, HelpText = "OutputDirectoryHelpText", ResourceType = typeof(Translations.Common))]
        public string OutputDirectory { get; set; }

        [Option("outputFilename", Required = false, Default = "result", HelpText = "OutputFilenameHelpText", ResourceType = typeof(Translations.Common))]
        public string OutputFilename { get; set; }

        [Option("githubPat", Required = false, HelpText = "GitHubPatHelpText", ResourceType = typeof(Translations.Common))]
        public string GitHubPat { get; set; }
    }
}

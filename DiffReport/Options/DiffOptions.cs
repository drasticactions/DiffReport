// <copyright file="DiffOptions.cs" company="Drastic Actions">
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
    [Verb("diff", HelpText = "DiffOptionHelpText", ResourceType = typeof(Translations.Common))]
    public class DiffOptions : BaseOptions
    {
        [Option("baseSha", Required = true, HelpText = "BaseShaHelpText", ResourceType = typeof(Translations.Common))]
        public string BaseSha { get; set; }

        [Option("compareSha", Required = false, HelpText = "CompareShaHelpText", ResourceType = typeof(Translations.Common))]
        public string CompareSha { get; set; }

        [Option("branch", Required = false, Default = "main", HelpText = "BranchDiffHelpText", ResourceType = typeof(Translations.Common))]
        public string Branch { get; set; }

        [Option("workingDir", Required = false, HelpText = "WorkingDirectoryDiffHelpText", ResourceType = typeof(Translations.Common))]
        public string WorkingDirectory { get; set; }
    }
}

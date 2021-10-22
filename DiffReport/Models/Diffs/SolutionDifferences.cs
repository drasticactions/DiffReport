// <copyright file="SolutionDifferences.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiffReport.Models.Diffs
{
    public class SolutionDifferences
    {
        public string SolutionFileName { get; set; }

        public string SolutionFileNameHeader => this.SolutionFileName.Replace(".", "-");

        public int FilesChanged => this.ProjectDifferences.SelectMany(n => n.Items).Count();

        public List<ProjectDifferences> ProjectDifferences { get; set; } = new List<ProjectDifferences>();
    }
}

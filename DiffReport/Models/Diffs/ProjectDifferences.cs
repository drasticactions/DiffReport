// <copyright file="ProjectDifferences.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiffReport.Models.Diffs
{
    public class ProjectDifferences
    {
        public string ProjectFileName { get; set; }

        public string ProjectFileNameHeader => this.ProjectFileName.Replace(".", "-");

        public int FilesChanged => this.Items.Count();

        public List<ProjectDifference> Items { get; set; } = new List<ProjectDifference>();

        public string CombindedDiff => string.Join(Environment.NewLine, this.Items.Select(n => n.Patch));
    }
}

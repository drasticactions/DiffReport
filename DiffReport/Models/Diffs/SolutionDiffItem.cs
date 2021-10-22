// <copyright file="SolutionDiffItem.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiffReport.Models.Diffs
{
    public class SolutionDiffItem
    {
        public string RepoName { get; set; }

        public string BranchName { get; set; }

        public string WorkingDirectory { get; set; }

        public string BaseCommitSha { get; set; }

        public string CompareCommitSha { get; set; }

        public List<SolutionDifferences> Solutions { get; set; }

        [JsonIgnore]
        public string LocalJson { get; set; }
    }
}

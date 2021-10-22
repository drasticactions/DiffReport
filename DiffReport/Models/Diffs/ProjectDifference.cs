// <copyright file="ProjectDifferences.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffReport.Models.Diffs
{
    public class ProjectDifference
    {
        public string Item { get; set; }

        public string Patch { get; set; }
    }
}

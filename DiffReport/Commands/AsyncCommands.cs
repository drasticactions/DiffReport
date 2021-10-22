// <copyright file="AsyncCommands.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DiffReport.Options;
using DiffReport.Tools;

namespace DiffReport.Commands
{
    public static class AsyncCommands
    {
        public static async Task<int> RunDiffCommandAsync(DiffOptions opts)
        {
            var diffCommand = new DiffCommand(opts);
            var workingDir = opts.WorkingDirectory ?? Directory.GetCurrentDirectory();
            var outputDir = opts.OutputDirectory ?? Directory.GetCurrentDirectory();
            var result = diffCommand.GenerateSolutionDiffItem(workingDir);
            result.LocalJson = JsonSerializer.Serialize(result);
            var templateHtml = Utilities.GenerateInternalTemplate("diff.html.hbs");
            var resultHtml = Utilities.GenerateHtmlTemplate(templateHtml, result);
            System.IO.File.WriteAllText(Path.Combine(outputDir, $"{Path.GetFileNameWithoutExtension(opts.OutputFilename)}.html"), resultHtml);
            System.IO.File.WriteAllText(Path.Combine(outputDir, $"{Path.GetFileNameWithoutExtension(opts.OutputFilename)}.json"), JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true }));
            return 0;
        }
    }
}

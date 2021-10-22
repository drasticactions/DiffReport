// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using CommandLine;
using DiffReport.Options;

namespace DiffReport
{
    /// <summary>
    /// Program Main.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main.
        /// </summary>
        /// <param name="args">Program Arguments.</param>
        private static int Main(string[] args)
        {
            Microsoft.Build.Locator.MSBuildLocator.RegisterDefaults();

            return Parser.Default.ParseArguments<DiffOptions>(args)
                      .MapResult(
                        (DiffOptions copyOptions) => RunDiffCommand(copyOptions),
                        errs => 1);
        }

        private static int RunDiffCommand(DiffOptions opts)
        {
            return Commands.AsyncCommands.RunDiffCommandAsync(opts).GetAwaiter().GetResult();
        }
    }
}

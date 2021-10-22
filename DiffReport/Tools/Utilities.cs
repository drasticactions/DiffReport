// <copyright file="Utilities.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HandlebarsDotNet;
using LibGit2Sharp;
using net.r_eg.MvsSln;

namespace DiffReport.Tools
{
    public static class Utilities
    {
        public static List<Sln> DiscoverSolutions(string directory)
        {
            var slnList = new ConcurrentBag<Sln>();

            var slnFiles = Directory.GetFiles(directory, "*.sln", SearchOption.AllDirectories);
            Parallel.ForEach(slnFiles, (slnFile) => {
                slnList.Add(new Sln(slnFile, SlnItems.EnvWithMinimalProjects));
            });

            return slnList.ToList();
        }

        public static string GenerateInternalTemplate(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "DiffReport.Templates." + fileName;

            string resource = null;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using StreamReader reader = new StreamReader(stream);
                resource = reader.ReadToEnd();
            }

            return resource;
        }

        public static string GenerateHtmlTemplate(string templateHtml, object viewModel)
        {
            var handlebars = Handlebars.Create();
            var template = handlebars.Compile(templateHtml);
            return template.Invoke(viewModel);
        }

        public static string Serialize(this object value)
        {
            return JsonSerializer.Serialize<object>(value);
        }
    }
}

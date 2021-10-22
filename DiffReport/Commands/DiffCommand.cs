// <copyright file="DiffCommand.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiffReport.Models.Diffs;
using DiffReport.Options;
using LibGit2Sharp;
using net.r_eg.MvsSln;
using net.r_eg.MvsSln.Core;

namespace DiffReport.Commands
{
    public class DiffCommand
    {
        private DiffOptions opts;

        public DiffCommand(DiffOptions opts)
        {
            this.opts = opts;
        }

        public SolutionDiffItem GenerateSolutionDiffItem(string workingDir)
        {
            using var repo = new Repository(workingDir);
            var branch = repo.Branches[this.opts.Branch];

            // Get two commits to diff.
            var commit1 = branch.Commits.First(n => n.Id == new ObjectId(this.opts.BaseSha));
            Commit? commit2;
            if (string.IsNullOrEmpty(this.opts.CompareSha))
            {
                commit2 = branch.Tip;
            }
            else
            {
                commit2 = branch.Commits.First(n => n.Id == new ObjectId(this.opts.CompareSha));
            }

            var diffTree1 = repo.Diff.Compare<Patch>(commit1.Tree, commit2.Tree);
            var name = string.Empty;
            try
            {
                name = repo.Network.Remotes.First().Url;
            }
            catch (Exception)
            {
            }
            var slnDiffer = new SolutionDiffItem()
            {
                RepoName = name,
                WorkingDirectory = repo.Info.WorkingDirectory,
                BaseCommitSha = commit1.Id.Sha,
                CompareCommitSha = commit2.Id.Sha,
                BranchName = branch.FriendlyName,
            };
            var slns = Tools.Utilities.DiscoverSolutions(repo.Info.WorkingDirectory);
            var slnDiffs = new ConcurrentBag<SolutionDifferences>();

            Parallel.ForEach(slns, (sln) =>
            {
                var slnDiff = this.GetSolutionDifferences(sln, diffTree1, workingDir);
                if (slnDiff.ProjectDifferences.Any())
                {
                    slnDiffs.Add(slnDiff);
                }
            });

            slnDiffer.Solutions = slnDiffs.ToList();
            return slnDiffer;
        }

        private SolutionDifferences GetSolutionDifferences(Sln sln, Patch diffs, string repoPath = "")
        {
            ConcurrentBag<ProjectDifferences> projDifferences = new ConcurrentBag<ProjectDifferences>();
            Parallel.ForEach(sln.Result.Env.Projects, (proj) => {
                var item = this.GetProjectDifferences(proj, diffs, repoPath);
                if (item.Items.Any())
                {
                    projDifferences.Add(item);
                }
            });

            return new SolutionDifferences() { ProjectDifferences = projDifferences.ToList(), SolutionFileName = Path.GetFileNameWithoutExtension(sln.Result.SolutionFile) };
        }

        private ProjectDifferences GetProjectDifferences(IXProject proj, Patch diffs, string repoPath = "")
        {
            ConcurrentBag<ProjectDifference> differences = new ConcurrentBag<ProjectDifference>();

            Parallel.ForEach(diffs, (diff) => {
                var item = proj.GetItems().Where(n => Path.GetFullPath(diff.Path, repoPath) == Path.Combine(proj.ProjectPath, n.evaluatedInclude));
                if (item.Any())
                {
                    differences.Add(new ProjectDifference() { Patch = diff.Patch, Item = item.First().evaluatedInclude });
                }
            });
            return new ProjectDifferences() { ProjectFileName = proj.ProjectName, Items = differences.ToList() };
        }
    }
}

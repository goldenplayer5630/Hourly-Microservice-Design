﻿using Hourly.GitService.Contracts.Responses.GitCommitResponses;

namespace Hourly.GitService.Contracts.Responses.GitRepositoryResponses
{
    public class GitRepositoryResponse : GitRepositorySummaryResponse
    {
        public ICollection<GitCommitSummaryResponse> GitCommits { get; init; } = new List<GitCommitSummaryResponse>();
    }
}

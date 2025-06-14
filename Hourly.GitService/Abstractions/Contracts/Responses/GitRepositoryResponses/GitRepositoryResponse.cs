using Hourly.GitService.Abstractions.Contracts.Responses.GitCommitResponses;

namespace Hourly.GitService.Abstractions.Contracts.Responses.GitRepositoryResponses
{
    public class GitRepositoryResponse : GitRepositorySummaryResponse
    {
        public ICollection<GitCommitSummaryResponse> GitCommits { get; set; } = new List<GitCommitSummaryResponse>();
    }
}

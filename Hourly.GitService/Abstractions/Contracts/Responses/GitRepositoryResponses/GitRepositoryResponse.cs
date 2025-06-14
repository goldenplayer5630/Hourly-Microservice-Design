using Hourly.GitService.Api.Contracts.Responses.GitCommitResponses;

namespace Hourly.GitService.Api.Contracts.Requests.GitRepositoryResponses
{
    public class GitRepositoryResponse : GitRepositorySummaryResponse
    {
        public ICollection<GitCommitSummaryResponse> GitCommits { get; set; } = new List<GitCommitSummaryResponse>();
    }
}

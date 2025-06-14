using Hourly.TimeTrackingService.Api.Contracts.Requests.GitCommitResponses;

namespace Hourly.TimeTrackingService.Api.Contracts.Requests.GitRepositoryResponses
{
    public class GitRepositoryResponse : GitRepositorySummaryResponse
    {
        public ICollection<GitCommitSummaryResponse> GitCommits { get; set; } = new List<GitCommitSummaryResponse>();
    }
}

using Hourly.TimeTrackingService.Contracts.Responses.GitCommitResponses;

namespace Hourly.TimeTrackingService.Contracts.Responses.GitRepositoryResponses
{
    public class GitRepositoryResponse : GitRepositorySummaryResponse
    {
        public ICollection<GitCommitSummaryResponse> GitCommits { get; init; } = new List<GitCommitSummaryResponse>();
    }
}

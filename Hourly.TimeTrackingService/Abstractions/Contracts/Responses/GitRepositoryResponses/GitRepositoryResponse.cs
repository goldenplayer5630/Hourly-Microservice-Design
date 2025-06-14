using Hourly.TimeTrackingService.Abstractions.Contracts.Responses.GitCommitResponses;

namespace Hourly.TimeTrackingService.Abstractions.Contracts.Responses.GitRepositoryResponses
{
    public class GitRepositoryResponse : GitRepositorySummaryResponse
    {
        public ICollection<GitCommitSummaryResponse> GitCommits { get; set; } = new List<GitCommitSummaryResponse>();
    }
}

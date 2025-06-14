using Hourly.TimeTrackingService.Abstractions.Contracts.Requests.UserContractResponses;
using Hourly.TimeTrackingService.Abstractions.Contracts.Responses.GitCommitResponses;

namespace Hourly.TimeTrackingService.Abstractions.Contracts.Responses.UserResponses
{
    public class UserResponse : UserSummaryResponse
    {
        public ICollection<GitCommitSummaryResponse> GitCommits { get; set; } = new List<GitCommitSummaryResponse>();

        public ICollection<UserContractSummaryResponse> Contracts { get; set; } = new List<UserContractSummaryResponse>();
    }
}

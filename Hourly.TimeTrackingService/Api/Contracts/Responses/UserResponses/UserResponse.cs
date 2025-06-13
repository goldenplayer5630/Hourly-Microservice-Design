using Hourly.TimeTrackingService.Api.Contracts.Requests.GitCommitResponses;
using Hourly.TimeTrackingService.Api.Contracts.Requests.UserContractResponses;
using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;

namespace Hourly.TimeTrackingService.Api.Contracts.Requests.UserResponses
{
    public class UserResponse : UserSummaryResponse
    {
        public ICollection<GitCommitSummaryResponse> GitCommits { get; set; } = new List<GitCommitSummaryResponse>();

        public ICollection<UserContractSummaryResponse> Contracts { get; set; } = new List<UserContractSummaryResponse>();
    }
}

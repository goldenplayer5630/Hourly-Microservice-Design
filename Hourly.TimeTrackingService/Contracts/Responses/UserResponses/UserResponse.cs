﻿using Hourly.TimeTrackingService.Contracts.Requests.UserContractResponses;
using Hourly.TimeTrackingService.Contracts.Responses.GitCommitResponses;

namespace Hourly.TimeTrackingService.Contracts.Responses.UserResponses
{
    public class UserResponse : UserSummaryResponse
    {
        public ICollection<GitCommitSummaryResponse> GitCommits { get; init; } = new List<GitCommitSummaryResponse>();

        public ICollection<UserContractSummaryResponse> Contracts { get; init; } = new List<UserContractSummaryResponse>();
    }
}

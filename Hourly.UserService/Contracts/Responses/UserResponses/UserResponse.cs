﻿using Hourly.UserService.Contracts.Responses.DepartmentResponses;
using Hourly.UserService.Contracts.Responses.GitCommitResponses;
using Hourly.UserService.Contracts.Responses.UserContractResponses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.UserService.Contracts.Responses.UserResponses
{
    public class UserResponse : UserSummaryResponse
    {
        [ForeignKey("DepartmentId")]
        public DepartmentSummaryResponse? Department { get; init; }

        public ICollection<GitCommitSummaryResponse> GitCommits { get; init; } = new List<GitCommitSummaryResponse>();

        public ICollection<UserContractSummaryResponse> Contracts { get; init; } = new List<UserContractSummaryResponse>();
    }
}

using Hourly.UserService.Abstractions.Contracts.Responses.DepartmentResponses;
using Hourly.UserService.Abstractions.Contracts.Responses.GitCommitResponses;
using Hourly.UserService.Abstractions.Contracts.Responses.RoleResponses;
using Hourly.UserService.Abstractions.Contracts.Responses.UserContractResponses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.UserService.Abstractions.Contracts.Responses.UserResponses
{
    public class UserResponse : UserSummaryResponse
    {
        [ForeignKey("RoleId")]
        public RoleSummaryResponse? Role { get; set; }

        [ForeignKey("DepartmentId")]
        public DepartmentSummaryResponse? Department { get; set; }

        public ICollection<GitCommitSummaryResponse> GitCommits { get; set; } = new List<GitCommitSummaryResponse>();

        public ICollection<UserContractSummaryResponse> Contracts { get; set; } = new List<UserContractSummaryResponse>();
    }
}

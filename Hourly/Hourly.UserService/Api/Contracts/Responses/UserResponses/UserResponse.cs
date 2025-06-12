using Hourly.Domain.Contracts.Responses.GitCommitResponses;
using Hourly.UserService.Api.Contracts.Responses.DepartmentResponses;
using Hourly.UserService.Api.Contracts.Responses.RoleResponses;
using Hourly.UserService.Api.Contracts.Responses.UserContractResponses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.UserService.Api.Contracts.Responses.UserResponses
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

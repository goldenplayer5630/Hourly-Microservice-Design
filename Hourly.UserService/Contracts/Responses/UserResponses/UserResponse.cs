using Hourly.UserService.Contracts.Responses.DepartmentResponses;
using Hourly.UserService.Contracts.Responses.GitCommitResponses;
using Hourly.UserService.Contracts.Responses.RoleResponses;
using Hourly.UserService.Contracts.Responses.UserContractResponses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.UserService.Contracts.Responses.UserResponses
{
    public class UserResponse : UserSummaryResponse
    {
        [ForeignKey("DepartmentId")]
        public DepartmentSummaryResponse? Department { get; set; }

        public ICollection<GitCommitSummaryResponse> GitCommits { get; set; } = new List<GitCommitSummaryResponse>();

        public ICollection<UserContractSummaryResponse> Contracts { get; set; } = new List<UserContractSummaryResponse>();
    }
}

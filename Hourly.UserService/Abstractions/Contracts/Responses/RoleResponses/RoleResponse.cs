using Hourly.UserService.Abstractions.Contracts.Responses.UserResponses;

namespace Hourly.UserService.Abstractions.Contracts.Responses.RoleResponses
{
    public class RoleResponse : RoleSummaryResponse
    {
        public ICollection<UserSummaryResponse> Users { get; set; } = new List<UserSummaryResponse>();
    }
}

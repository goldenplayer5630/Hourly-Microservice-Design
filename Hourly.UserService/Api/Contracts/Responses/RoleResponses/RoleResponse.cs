using Hourly.UserService.Api.Contracts.Responses.UserResponses;

namespace Hourly.UserService.Api.Contracts.Responses.RoleResponses
{
    public class RoleResponse : RoleSummaryResponse
    {
        public ICollection<UserSummaryResponse> Users { get; set; } = new List<UserSummaryResponse>();
    }
}

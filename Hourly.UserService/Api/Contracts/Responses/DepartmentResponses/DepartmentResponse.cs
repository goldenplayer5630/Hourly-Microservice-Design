using Hourly.UserService.Api.Contracts.Responses.UserResponses;

namespace Hourly.UserService.Api.Contracts.Responses.DepartmentResponses
{
    public class DepartmentResponse : DepartmentSummaryResponse
    {
        public ICollection<UserSummaryResponse> Users { get; set; } = new List<UserSummaryResponse>();
    }
}

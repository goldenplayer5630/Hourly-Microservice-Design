using Hourly.UserService.Contracts.Responses.UserResponses;

namespace Hourly.UserService.Contracts.Responses.DepartmentResponses
{
    public class DepartmentResponse : DepartmentSummaryResponse
    {
        public ICollection<UserSummaryResponse> Users { get; init; } = new List<UserSummaryResponse>();
    }
}

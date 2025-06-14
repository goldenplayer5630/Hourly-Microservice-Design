using Hourly.UserService.Abstractions.Contracts.Responses.UserResponses;

namespace Hourly.UserService.Abstractions.Contracts.Responses.DepartmentResponses
{
    public class DepartmentResponse : DepartmentSummaryResponse
    {
        public ICollection<UserSummaryResponse> Users { get; set; } = new List<UserSummaryResponse>();
    }
}

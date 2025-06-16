using Hourly.UserService.Contracts.Responses.UserResponses;
using Hourly.UserService.Contracts.Responses.WorkSessionResponses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.UserService.Contracts.Responses.UserContractResponses
{
    public class UserContractResponse : UserContractSummaryResponse
    {
        // Navigation properties
        [ForeignKey("UserId")]
        public UserSummaryResponse? User { get; init; }

        public ICollection<WorkSessionSummaryResponse> WorkSessions { get; init; } = new List<WorkSessionSummaryResponse>();
    }
}

using Hourly.TimeTrackingService.Contracts.Responses.UserResponses;
using Hourly.TimeTrackingService.Contracts.Responses.WorkSessionResponses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.TimeTrackingService.Contracts.Requests.UserContractResponses
{
    public class UserContractResponse : UserContractSummaryResponse
    {
        // Navigation properties
        [ForeignKey("UserId")]
        public UserSummaryResponse User { get; set; }

        public ICollection<WorkSessionSummaryResponse> WorkSessions { get; set; } = new List<WorkSessionSummaryResponse>();
    }
}

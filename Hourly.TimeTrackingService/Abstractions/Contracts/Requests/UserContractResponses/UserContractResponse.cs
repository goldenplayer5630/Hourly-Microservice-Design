using Hourly.TimeTrackingService.Api.Contracts.Requests.UserResponses;
using Hourly.TimeTrackingService.Api.Contracts.Requests.WorkSessionResponses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.TimeTrackingService.Api.Contracts.Requests.UserContractResponses
{
    public class UserContractResponse : UserContractSummaryResponse
    {
        // Navigation properties
        [ForeignKey("UserId")]
        public UserSummaryResponse User { get; set; }

        public ICollection<WorkSessionSummaryResponse> WorkSessions { get; set; } = new List<WorkSessionSummaryResponse>();
    }
}

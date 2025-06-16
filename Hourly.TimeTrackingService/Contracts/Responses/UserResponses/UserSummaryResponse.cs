using System.ComponentModel.DataAnnotations;

namespace Hourly.TimeTrackingService.Contracts.Responses.UserResponses
{
    public class UserSummaryResponse
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public string Name { get; init; } = string.Empty;

        [Required]
        public float TVTHourBalance { get; init; } 
    }
}

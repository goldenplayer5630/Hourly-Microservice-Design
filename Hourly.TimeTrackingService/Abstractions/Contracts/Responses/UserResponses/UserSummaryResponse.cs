using System.ComponentModel.DataAnnotations;

namespace Hourly.TimeTrackingService.Abstractions.Contracts.Responses.UserResponses
{
    public class UserSummaryResponse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float TVTHourBalance { get; set; }
    }
}

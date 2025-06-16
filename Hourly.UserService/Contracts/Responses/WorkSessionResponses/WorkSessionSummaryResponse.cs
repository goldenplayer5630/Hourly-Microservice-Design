using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Contracts.Responses.WorkSessionResponses
{
    public class WorkSessionSummaryResponse
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public Guid UserContractId { get; init; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Hourly.Domain.Contracts.Responses.WorkSessionResponses
{
    public class WorkSessionSummaryResponse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserContractId { get; set; }
    }
}
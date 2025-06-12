using System.ComponentModel.DataAnnotations;
using System.Numerics;

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
using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Api.Contracts.Responses.DepartmentResponses
{
    public class DepartmentSummaryResponse
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Contracts.Responses.RoleResponses
{
    public class RoleSummaryResponse
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Permissions { get; set; } // Store as JSON string

        [Required]
        public DateTime CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }
    }
}

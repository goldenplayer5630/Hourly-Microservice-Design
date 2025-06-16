using Hourly.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.UserService.Contracts.Responses.UserResponses
{
    public class UserSummaryResponse
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public string Name { get; init; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; init; } = string.Empty;

        public UserRole Role { get; init; }

        public Guid? DepartmentId { get; init; }

        public string? GitEmail { get; init; }

        public string? GitUsername { get; init; }

        public string? GitAccessToken { get; init; }

        [Required]
        public float TVTHourBalance { get; init; }

        [Required]
        public DateTime CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }
    }
}

using Hourly.Shared.Enums;
using Hourly.UserService.Contracts.Responses.RoleResponses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.UserService.Contracts.Responses.UserResponses
{
    public class UserSummaryResponse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public UserRole Role { get; set; }

        public Guid? DepartmentId { get; set; }

        public string? GitEmail { get; set; }

        public string? GitUsername { get; set; }

        public string? GitAccessToken { get; set; }

        [Required]
        public float TVTHourBalance { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}

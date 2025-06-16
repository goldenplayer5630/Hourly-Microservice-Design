using Hourly.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Contracts.Requests.UserRequests
{
    public class UpdateUserRequest
    {
        [Required]
        public string Name { get; init; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; init; } = string.Empty;

        [Required]
        public UserRole Role { get; init; }

        public string? GitEmail { get; init; }

        public string? GitUsername { get; init; }

        public string? GitAccessToken { get; init; }

        [Required]
        public float TVTHourBalance { get; init; }
    }
}

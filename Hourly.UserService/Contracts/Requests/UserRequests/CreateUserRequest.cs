using Hourly.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Contracts.Requests.UserRequests
{
    public class CreateUserRequest
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public string? GitEmail { get; set; }

        public string? GitUsername { get; set; }

        public string? GitAccessToken { get; set; }

        [Required]
        public float TVTHourBalance { get; set; }

    }
}

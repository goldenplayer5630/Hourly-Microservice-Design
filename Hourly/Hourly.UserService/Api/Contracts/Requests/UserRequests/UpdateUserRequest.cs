using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Api.Contracts.Requests.UserRequests
{
    public class UpdateUserRequest
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        public string? GitEmail { get; set; }

        public string? GitUsername { get; set; }

        public string? GitAccessToken { get; set; }

        [Required]
        public float TVTHourBalance { get; set; }
    }
}

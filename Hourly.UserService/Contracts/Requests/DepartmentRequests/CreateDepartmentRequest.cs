using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Contracts.Requests.DepartmentRequests
{
    public class CreateDepartmentRequest
    {
        [Required]
        public string Name { get; init; } = string.Empty;
    }
}

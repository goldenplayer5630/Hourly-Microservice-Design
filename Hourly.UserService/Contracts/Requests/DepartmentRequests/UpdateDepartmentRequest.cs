using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Contracts.Requests.DepartmentRequests
{
    public class UpdateDepartmentRequest
    {
        [Required]
        public string Name { get; init; } = string.Empty;
    }
}

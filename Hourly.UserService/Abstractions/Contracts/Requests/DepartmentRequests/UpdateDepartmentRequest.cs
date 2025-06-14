using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Abstractions.Contracts.Requests.DepartmentRequests
{
    public class UpdateDepartmentRequest
    {
        [Required]
        public string Name { get; set; }
    }
}

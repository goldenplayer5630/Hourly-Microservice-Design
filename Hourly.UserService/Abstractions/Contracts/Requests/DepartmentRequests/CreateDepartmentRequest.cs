using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Abstractions.Contracts.Requests.DepartmentRequests
{
    public class CreateDepartmentRequest
    {
        [Required]
        public string Name { get; set; }
    }
}

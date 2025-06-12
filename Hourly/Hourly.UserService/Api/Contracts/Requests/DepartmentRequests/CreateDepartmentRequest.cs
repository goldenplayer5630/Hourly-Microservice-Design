using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Api.Contracts.Requests.DepartmentRequests
{
    public class CreateDepartmentRequest
    {
        [Required]
        public string Name { get; set; }
    }
}

using Hourly.UserService.Api.Contracts.Requests.DepartmentRequests;
using Hourly.UserService.Api.Contracts.Responses.DepartmentResponses;
using Hourly.UserService.Domain.Entities;

namespace Hourly.UserService.Application.Mappers
{
    public static partial class DepartmentMapper
    {
        public static DepartmentResponse ToResponse(this Department department)
        {
            return new DepartmentResponse
            {
                Id = department.Id,
                Name = department.Name,
                Users = department.Users.Select(user => user.ToSummaryResponse()).ToList(),
                CreatedAt = department.CreatedAt,
                UpdatedAt = department.UpdatedAt
            };
        }

        public static DepartmentSummaryResponse ToSummaryResponse(this Department department)
        {
            return new DepartmentSummaryResponse
            {
                Id = department.Id,
                Name = department.Name,
                CreatedAt = department.CreatedAt,
                UpdatedAt = department.UpdatedAt
            };
        }

        public static Department ToDepartment(this CreateDepartmentRequest request)
        {
            return new Department
            {
                Name = request.Name
            };
        }

        public static Department ToDepartment(this UpdateDepartmentRequest request, Guid id)
        {
            return new Department
            {
                Id = id,
                Name = request.Name
            };
        }

    }
}

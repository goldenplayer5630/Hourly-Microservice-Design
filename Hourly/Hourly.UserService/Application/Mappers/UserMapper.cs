using Hourly.UserService.Api.Contracts.Requests.UserRequests;
using Hourly.UserService.Api.Contracts.Responses.UserResponses;
using Hourly.UserService.Application.Mappers;
using Hourly.UserService.Domain.Entities;

namespace Hourly.UserService.Application.Mappers
{
    public static partial class UserMapper
    {
        public static UserResponse ToResponse(this User entity)
        {
            return new UserResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                RoleId = entity.RoleId,
                Role = entity.Role.ToSummaryResponse(),
                DepartmentId = entity.DepartmentId,
                Department = entity.Department?.ToSummaryResponse(),
                GitEmail = entity.GitEmail,
                GitUsername = entity.GitUsername,
                GitAccessToken = entity.GitAccessToken,
                TVTHourBalance = entity.TVTHourBalance,
                GitCommits = entity.GitCommits.Select(gc => gc.ToSummaryResponse()).ToList(),
                Contracts = entity.Contracts.Select(uc => uc.ToSummaryResponse()).ToList(),
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static UserSummaryResponse ToSummaryResponse(this User entity)
        {
            return new UserSummaryResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                RoleId = entity.RoleId,
                Role = entity.Role.ToSummaryResponse(),
                DepartmentId = entity.DepartmentId,
                GitEmail = entity.GitEmail,
                GitUsername = entity.GitUsername,
                GitAccessToken = entity.GitAccessToken,
                TVTHourBalance = entity.TVTHourBalance,
            };
        }

        public static User ToUser(this CreateUserRequest request)
        {
            return new User
            {
                Name = request.Name,
                Email = request.Email,
                GitEmail = request.GitEmail,
                GitUsername = request.GitUsername,
                GitAccessToken = request.GitAccessToken,
                TVTHourBalance = request.TVTHourBalance,
                RoleId = request.RoleId,
            };
        }

        public static User ToUser(this UpdateUserRequest request, Guid id)
        {
            return new User
            {
                Id = id,
                Name = request.Name,
                Email = request.Email,
                GitEmail = request.GitEmail,
                GitUsername = request.GitUsername,
                GitAccessToken = request.GitAccessToken,
                TVTHourBalance = request.TVTHourBalance,
                RoleId = request.RoleId,
            };
        }
    }
}

using Hourly.UserService.Abstractions.Contracts.Responses.RoleResponses;
using Hourly.UserService.Domain.Entities;

namespace Hourly.UserService.Application.Mappers
{
    public static partial class RoleMapper
    {
        public static RoleResponse ToResponse(this Role entity)
        {
            return new RoleResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Permissions = entity.Permissions,
                Users = entity.Users.Select(u => u.ToSummaryResponse()).ToList(),
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static RoleSummaryResponse ToSummaryResponse(this Role entity)
        {
            return new RoleSummaryResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Permissions = entity.Permissions,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}

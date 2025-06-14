using Hourly.GitService.Infrastructure.Persistence.ReadModels;

namespace Hourly.GitService.Abstractions.Queries
{
    public interface IUserQuery
    {
        Task<UserReadModel?> GetById(Guid userId);
    }
}

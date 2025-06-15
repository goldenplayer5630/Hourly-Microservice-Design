using Hourly.GitService.Infrastructure.Persistence.ReadModels;

namespace Hourly.GitService.Infrastructure.Queries
{
    public interface IUserQuery
    {
        Task<UserReadModel?> GetById(Guid userId);
    }
}

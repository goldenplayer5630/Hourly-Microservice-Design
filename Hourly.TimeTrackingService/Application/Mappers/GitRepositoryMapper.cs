using Hourly.TimeTrackingService.Api.Contracts.Requests.GitRepositoryResponses;
using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;

namespace Hourly.TimeTrackingService.Application.Mappers
{
    public static partial class GitRepositoryMapper
    {
        public static GitRepositoryResponse ToResponse(this GitRepositoryReadModel entity)
        {
            return new GitRepositoryResponse
            {
                Id = entity.Id,
                ExtRepositoryId = entity.ExtRepositoryId,
                Name = entity.Name,
                WebUrl = entity.WebUrl,
                GitCommits = entity.GitCommits.Select(gc => gc.ToSummaryResponse()).ToList(),
            };
        }

        public static GitRepositorySummaryResponse ToSummaryResponse(this GitRepositoryReadModel entity)
        {
            return new GitRepositorySummaryResponse
            {
                Id = entity.Id,
                ExtRepositoryId = entity.ExtRepositoryId,
                Name = entity.Name,
                WebUrl = entity.WebUrl,
            };
        }
    }
}

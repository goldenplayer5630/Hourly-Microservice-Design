using Hourly.GitService.Api.Contracts.Requests.GitRepositoryRequests;
using Hourly.GitService.Api.Contracts.Requests.GitRepositoryResponses;
using Hourly.GitService.Domain.Entities;


namespace Hourly.GitService.Application.Mappers
{
    public static partial class GitRepositoryMapper
    {
        public static GitRepositoryResponse ToResponse(this GitRepository entity)
        {
            return new GitRepositoryResponse
            {
                Id = entity.Id,
                ExtRepositoryId = entity.ExtRepositoryId,
                Name = entity.Name,
                Namespace = entity.Namespace,
                WebUrl = entity.WebUrl,
                GitCommits = entity.GitCommits.Select(gc => gc.ToSummaryResponse()).ToList(),
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static GitRepositorySummaryResponse ToSummaryResponse(this GitRepository entity)
        {
            return new GitRepositorySummaryResponse
            {
                Id = entity.Id,
                ExtRepositoryId = entity.ExtRepositoryId,
                Name = entity.Name,
                Namespace = entity.Namespace,
                WebUrl = entity.WebUrl,
            };
        }

        public static GitRepository ToGitRepository(this CreateGitRepositoryRequest request)
        {
            return new GitRepository
            {
                ExtRepositoryId = request.ExtRepositoryId,
                Name = request.Name,
                Namespace = request.Namespace,
                WebUrl = request.WebUrl
            };
        }

        public static GitRepository ToGitRepository(this UpdateGitRepositoryRequest request, Guid id)
        {
            return new GitRepository
            {
                Id = id,
                ExtRepositoryId = request.ExtRepositoryId,
                Name = request.Name,
                Namespace = request.Namespace,
                WebUrl = request.WebUrl
            };
        }
    }
}

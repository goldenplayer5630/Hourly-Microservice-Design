using Hourly.Domain.Mappers;
using Hourly.TimeTrackingService.Abstractions.Contracts.Responses.GitCommitResponses;
using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;

namespace Hourly.TimeTrackingService.Application.Mappers
{
    public static partial class GitCommitMapper
    {
        public static GitCommitResponse ToResponse(this GitCommitReadModel entity)
        {
            return new GitCommitResponse
            {
                Id = entity.Id,
                RepositoryId = entity.GitRepositoryId,
                Repository = entity.GitRepository.ToSummaryResponse(),
                ExtCommitId = entity.ExtCommitId,
                ExtCommitShortId = entity.ExtCommitShortId,
                Title = entity.Title,
                AuthorId = entity.AuthorId,
                Author = entity.Author.ToSummaryResponse(),
                AuthoredDate = entity.AuthoredDate,
                WebUrl = entity.WebUrl,
                WorkSessions = entity.WorkSessions.Select(ws => ws.ToSummaryResponse()).ToList(),
            };
        }

        public static GitCommitSummaryResponse ToSummaryResponse(this GitCommitReadModel entity)
        {
            return new GitCommitSummaryResponse
            {
                Id = entity.Id,
                RepositoryId = entity.GitRepositoryId,
                ExtCommitId = entity.ExtCommitId,
                ExtCommitShortId = entity.ExtCommitShortId,
                Title = entity.Title,
                AuthorId = entity.AuthorId,
                WebUrl = entity.WebUrl,
            };
        }
    }
}

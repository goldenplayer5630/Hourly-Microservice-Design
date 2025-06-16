using Hourly.GitService.Contracts.Requests.GitCommitRequests;
using Hourly.GitService.Contracts.Responses.GitCommitResponses;
using Hourly.GitService.Domain.Entities;

namespace Hourly.GitService.Application.Mappers
{
    public static partial class GitCommitMapper
    {
        public static GitCommitResponse ToResponse(this GitCommit entity)
        {
            return new GitCommitResponse
            {
                Id = entity.Id,
                GitRepositoryId = entity.GitRepositoryId,
                GitRepository = entity.GitRepository.ToSummaryResponse(),
                ExtCommitId = entity.ExtCommitId,
                ExtCommitShortId = entity.ExtCommitShortId,
                Title = entity.Title,
                Comment = entity.Comment,
                AuthorId = entity.AuthorId,
                AuthoredDate = entity.AuthoredDate,
                WebUrl = entity.WebUrl,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static GitCommitSummaryResponse ToSummaryResponse(this GitCommit entity)
        {
            return new GitCommitSummaryResponse
            {
                Id = entity.Id,
                GitRepositoryId = entity.GitRepositoryId,
                ExtCommitId = entity.ExtCommitId,
                ExtCommitShortId = entity.ExtCommitShortId,
                Title = entity.Title,
                Comment = entity.Comment,
                AuthorId = entity.AuthorId,
                AuthoredDate = entity.AuthoredDate,
                WebUrl = entity.WebUrl,
            };
        }

        public static GitCommit ToGitCommit(this CreateGitCommitRequest response)
        {
            return new GitCommit
            {
                GitRepositoryId = response.GitRepositoryId,
                ExtCommitId = response.ExtCommitId,
                ExtCommitShortId = response.ExtCommitShortId,
                Title = response.Title,
                Comment = response.Comment,
                AuthorId = response.AuthorId,
                AuthoredDate = response.AuthoredDate,
                WebUrl = response.WebUrl,
            };
        }
    }
}

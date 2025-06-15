using Hourly.UserService.Contracts.Responses.GitCommitResponses;
using Hourly.UserService.Infrastructure.Persistence.ReadModels;

namespace Hourly.UserService.Application.Mappers
{
    public static partial class GitCommitMapper
    {
        public static GitCommitResponse ToResponse(this GitCommitReadModel entity)
        {
            return new GitCommitResponse
            {
                Id = entity.Id,
                AuthorId = entity.AuthorId,
                Author = entity.Author.ToSummaryResponse(),
            };
        }

        public static GitCommitSummaryResponse ToSummaryResponse(this GitCommitReadModel entity)
        {
            return new GitCommitSummaryResponse
            {
                Id = entity.Id,
                AuthorId = entity.AuthorId,
            };
        }
    }
}

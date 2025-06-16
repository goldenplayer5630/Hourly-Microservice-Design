using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hourly.Shared.Events
{
    public record GitCommitCreatedEvent(Guid Id, Guid AuthorId, DateTime AuthoredDate, Guid GitRepositoryId, string ExtCommitId, string ExtCommitShortId, string Title, string WebUrl);
    public record GitCommitDeletedEvent(Guid Id);
}
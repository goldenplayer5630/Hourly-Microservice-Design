namespace Hourly.Shared.Events
{
    public record GitCommitCreatedEvent(Guid Id, Guid? AuthorId);
    public record GitCommitDeletedEvent(Guid Id);
}

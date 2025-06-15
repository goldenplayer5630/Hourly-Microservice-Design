namespace Hourly.UserService.Abstractions.Events
{
    public record GitCommitCreatedEvent(Guid Id, Guid AuthorId);
    public record GitCommitDeletedEvent(Guid Id);
}

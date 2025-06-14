namespace Hourly.UserService.Abstractions.Events
{
    public record WorkSessionCreatedEvent(Guid id, Guid userContractId);
    public record WorkSessionUpdatedEvent(Guid id, Guid userContractId);
    public record WorkSessionDeletedEvent(Guid id);
}

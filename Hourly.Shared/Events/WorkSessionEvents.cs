namespace Hourly.Shared.Events
{
    public record WorkSessionCreatedEvent(Guid Id, Guid UserContractId);
    public record WorkSessionUpdatedEvent(Guid Id, Guid UserContractId);
    public record WorkSessionDeletedEvent(Guid Id);
}

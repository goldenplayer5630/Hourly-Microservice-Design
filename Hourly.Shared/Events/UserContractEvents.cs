using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Abstractions.Events
{
    public record UserContractCreatedEvent(Guid Id, Guid UserId, string Name);
    public record UserContractUpdatedEvent(Guid Id, Guid UserId, string Name);
    public record UserContractDeletedEvent(Guid Id);
}
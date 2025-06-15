using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Abstractions.Events
{
    public record UserCreatedEvent(Guid Id, string Name, string Email, float TVTHourBalance, string? GitEmail, string? GitUserName, string? GitAccessToken);
    public record UserUpdatedEvent(Guid Id, string Name, string Email, float TVTHourBalance, string? GitEmail, string? GitUserName, string? GitAccessToken);
    public record UserDeletedEvent(Guid Id);
}
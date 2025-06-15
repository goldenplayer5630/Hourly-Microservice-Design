using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Shared.Events
{
    public record GitRepositoryCreatedEvent(
        Guid Id,
        string ExtRepositoryId,
        string Name,
        string WebUrl
    );

    public record GitRepositoryUpdatedEvent(
        Guid Id,
        string ExtRepositoryId,
        string Name,
        string WebUrl
    );

    public record GitRepositoryDeletedEvent(
        Guid Id
    );
}
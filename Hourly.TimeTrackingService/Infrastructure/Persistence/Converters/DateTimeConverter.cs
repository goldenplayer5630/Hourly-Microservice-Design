using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hourly.TimeTrackingService.Infrastructure.Persistence.Converters
{
    internal static class DateTimeConverter
    {
        public static readonly ValueConverter<DateTime, DateTime> UtcDateTimeConverter =
            new ValueConverter<DateTime, DateTime>(
                v => DateTime.SpecifyKind(v.ToUniversalTime(), DateTimeKind.Utc), // write
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // read
            );

        public static readonly ValueConverter<DateTime?, DateTime?> NullableUtcDateTimeConverter =
            new ValueConverter<DateTime?, DateTime?>(
                v => v.HasValue ? DateTime.SpecifyKind(v.Value.ToUniversalTime(), DateTimeKind.Utc) : v,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v
            );

    }
}

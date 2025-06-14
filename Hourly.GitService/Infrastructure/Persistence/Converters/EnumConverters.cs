using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hourly.GitService.Infrastructure.Persistence.Converters
{
    internal static class EnumConverters
    {
        public static ValueConverter<TEnum, string> ToStringConverter<TEnum>() where TEnum : struct, Enum
        {
            return new ValueConverter<TEnum, string>(
                v => v.ToString(),
                v => Enum.Parse<TEnum>(v));
        }
    }
}

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Portfolio.Infrastructure.Persistence.Converters;

/// <summary>
/// Ensures <see cref="DateTime"/> values are written to MySQL as UTC and read back with
/// <see cref="DateTimeKind.Utc"/>. Values without a kind are assumed to already be UTC,
/// which avoids an unintended local-time conversion.
/// </summary>
public sealed class UtcDateTimeConverter : ValueConverter<DateTime, DateTime>
{
    public UtcDateTimeConverter()
        : base(
            value => value.Kind == DateTimeKind.Utc
                ? value
                : value.Kind == DateTimeKind.Unspecified
                    ? DateTime.SpecifyKind(value, DateTimeKind.Utc)
                    : value.ToUniversalTime(),
            value => DateTime.SpecifyKind(value, DateTimeKind.Utc))
    {
    }
}

using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public class SiteSetting : IUpdatedAtEntity
{
    public long Id { get; set; }

    public string SettingKey { get; set; } = string.Empty;

    public string? SettingValue { get; set; }

    public DateTime UpdatedAt { get; set; }
}

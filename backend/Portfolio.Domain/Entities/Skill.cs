namespace Portfolio.Domain.Entities;

public class Skill
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string? IconUrl { get; set; }

    public int? ProficiencyLevel { get; set; }

    public int DisplayOrder { get; set; }
}

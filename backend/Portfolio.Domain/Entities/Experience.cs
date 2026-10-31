namespace Portfolio.Domain.Entities;

public class Experience
{
    public long Id { get; set; }

    public string CompanyName { get; set; } = string.Empty;

    public string Position { get; set; } = string.Empty;

    public string EmploymentType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public bool IsCurrent { get; set; }

    public int DisplayOrder { get; set; }
}

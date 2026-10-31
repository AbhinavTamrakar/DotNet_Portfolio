namespace Portfolio.Domain.Entities;

public class Education
{
    public long Id { get; set; }

    public string Institution { get; set; } = string.Empty;

    public string Qualification { get; set; } = string.Empty;

    public string FieldOfStudy { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int DisplayOrder { get; set; }
}

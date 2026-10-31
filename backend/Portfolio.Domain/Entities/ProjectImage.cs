namespace Portfolio.Domain.Entities;

public class ProjectImage
{
    public long Id { get; set; }

    public long ProjectId { get; set; }

    public Project Project { get; set; } = null!;

    public string ImageUrl { get; set; } = string.Empty;

    public string AltText { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }
}

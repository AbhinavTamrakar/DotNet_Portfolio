using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public class Project : IAuditableEntity
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string ShortDescription { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ThumbnailUrl { get; set; } = string.Empty;

    public string? GithubUrl { get; set; }

    public string? LiveDemoUrl { get; set; }

    public string Category { get; set; } = string.Empty;

    public bool IsFeatured { get; set; }

    public bool IsPublished { get; set; }

    public int DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ICollection<ProjectImage> Images { get; set; } = new List<ProjectImage>();

    public ICollection<ProjectTechnology> ProjectTechnologies { get; set; } = new List<ProjectTechnology>();
}

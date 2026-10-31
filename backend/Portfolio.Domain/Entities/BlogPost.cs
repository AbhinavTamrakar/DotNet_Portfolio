using Portfolio.Domain.Common;
using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Entities;

public class BlogPost : IAuditableEntity
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string Excerpt { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string FeaturedImageUrl { get; set; } = string.Empty;

    public string? SeoTitle { get; set; }

    public string? SeoDescription { get; set; }

    public BlogPostStatus Status { get; set; } = BlogPostStatus.Draft;

    public DateTime? PublishedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ICollection<BlogPostCategory> Categories { get; set; } = new List<BlogPostCategory>();

    public ICollection<BlogPostTag> Tags { get; set; } = new List<BlogPostTag>();
}

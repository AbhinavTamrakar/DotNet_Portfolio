namespace Portfolio.Domain.Entities;

public class BlogTag
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public ICollection<BlogPostTag> Posts { get; set; } = new List<BlogPostTag>();
}

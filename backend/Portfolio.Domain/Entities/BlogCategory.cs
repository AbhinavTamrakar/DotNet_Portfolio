namespace Portfolio.Domain.Entities;

public class BlogCategory
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public ICollection<BlogPostCategory> Posts { get; set; } = new List<BlogPostCategory>();
}

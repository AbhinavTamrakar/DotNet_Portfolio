namespace Portfolio.Domain.Entities;

public class BlogPostTag
{
    public long BlogId { get; set; }

    public BlogPost Blog { get; set; } = null!;

    public long TagId { get; set; }

    public BlogTag Tag { get; set; } = null!;
}

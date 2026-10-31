namespace Portfolio.Domain.Entities;

public class BlogPostCategory
{
    public long BlogId { get; set; }

    public BlogPost Blog { get; set; } = null!;

    public long CategoryId { get; set; }

    public BlogCategory Category { get; set; } = null!;
}

namespace Portfolio.Domain.Entities;

public class SocialLink
{
    public long Id { get; set; }

    public string Platform { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }

    public bool IsVisible { get; set; }
}

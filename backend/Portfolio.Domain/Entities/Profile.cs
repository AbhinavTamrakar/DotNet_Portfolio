using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

/// <summary>The portfolio owner's profile. A single active record is supported.</summary>
public class Profile : IAuditableEntity
{
    public long Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string ProfessionalTitle { get; set; } = string.Empty;

    public string ShortBio { get; set; } = string.Empty;

    public string Biography { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string ProfileImageUrl { get; set; } = string.Empty;

    public string CvFileUrl { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}

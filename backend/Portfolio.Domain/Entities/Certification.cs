namespace Portfolio.Domain.Entities;

public class Certification
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string IssuingOrganization { get; set; } = string.Empty;

    public string? CredentialId { get; set; }

    public string? CredentialUrl { get; set; }

    public string? CertificateUrl { get; set; }

    public DateOnly? IssueDate { get; set; }

    public DateOnly? ExpirationDate { get; set; }
}

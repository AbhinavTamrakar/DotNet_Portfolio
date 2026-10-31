using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence.Configurations;

internal sealed class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("Skills");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(120).IsRequired();
        builder.Property(x => x.Category).HasMaxLength(100).IsRequired();
        builder.Property(x => x.IconUrl).HasMaxLength(1000);

        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => x.DisplayOrder);
    }
}

internal sealed class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
{
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder.ToTable("Experiences");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CompanyName).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Position).HasMaxLength(200).IsRequired();
        builder.Property(x => x.EmploymentType).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Description).HasColumnType("longtext").IsRequired();

        builder.Property(x => x.StartDate).HasColumnType("date").IsRequired();
        builder.Property(x => x.EndDate).HasColumnType("date");

        builder.HasIndex(x => x.DisplayOrder);
    }
}

internal sealed class EducationConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.ToTable("Education");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Institution).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Qualification).HasMaxLength(200).IsRequired();
        builder.Property(x => x.FieldOfStudy).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Description).HasColumnType("text").IsRequired();

        builder.Property(x => x.StartDate).HasColumnType("date").IsRequired();
        builder.Property(x => x.EndDate).HasColumnType("date");

        builder.HasIndex(x => x.DisplayOrder);
    }
}

internal sealed class CertificationConfiguration : IEntityTypeConfiguration<Certification>
{
    public void Configure(EntityTypeBuilder<Certification> builder)
    {
        builder.ToTable("Certifications");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
        builder.Property(x => x.IssuingOrganization).HasMaxLength(200).IsRequired();
        builder.Property(x => x.CredentialId).HasMaxLength(200);
        builder.Property(x => x.CredentialUrl).HasMaxLength(1000);
        builder.Property(x => x.CertificateUrl).HasMaxLength(1000);

        builder.Property(x => x.IssueDate).HasColumnType("date");
        builder.Property(x => x.ExpirationDate).HasColumnType("date");

        builder.HasIndex(x => x.IssueDate);
    }
}

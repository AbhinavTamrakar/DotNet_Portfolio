using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence.Configurations;

internal sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Slug).HasMaxLength(220).IsRequired();
        builder.Property(x => x.ShortDescription).HasColumnType("text").IsRequired();
        builder.Property(x => x.Description).HasColumnType("longtext").IsRequired();
        builder.Property(x => x.ThumbnailUrl).HasMaxLength(1000).IsRequired();
        builder.Property(x => x.GithubUrl).HasMaxLength(1000);
        builder.Property(x => x.LiveDemoUrl).HasMaxLength(1000);
        builder.Property(x => x.Category).HasMaxLength(100).IsRequired();

        builder.Property(x => x.CreatedAt).HasColumnType("datetime(6)").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnType("datetime(6)").IsRequired();

        builder.HasIndex(x => x.Slug).IsUnique();
        builder.HasIndex(x => x.IsPublished);
        builder.HasIndex(x => x.IsFeatured);
        builder.HasIndex(x => x.DisplayOrder);
    }
}

internal sealed class ProjectImageConfiguration : IEntityTypeConfiguration<ProjectImage>
{
    public void Configure(EntityTypeBuilder<ProjectImage> builder)
    {
        builder.ToTable("ProjectImages");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ImageUrl).HasMaxLength(1000).IsRequired();
        builder.Property(x => x.AltText).HasMaxLength(255).IsRequired();

        builder.HasOne(x => x.Project)
            .WithMany(x => x.Images)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.ProjectId);
    }
}

internal sealed class TechnologyConfiguration : IEntityTypeConfiguration<Technology>
{
    public void Configure(EntityTypeBuilder<Technology> builder)
    {
        builder.ToTable("Technologies");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Slug).HasMaxLength(120).IsRequired();
        builder.Property(x => x.IconUrl).HasMaxLength(1000);

        builder.HasIndex(x => x.Slug).IsUnique();
    }
}

internal sealed class ProjectTechnologyConfiguration : IEntityTypeConfiguration<ProjectTechnology>
{
    public void Configure(EntityTypeBuilder<ProjectTechnology> builder)
    {
        builder.ToTable("ProjectTechnologies");
        builder.HasKey(x => new { x.ProjectId, x.TechnologyId });

        builder.HasOne(x => x.Project)
            .WithMany(x => x.ProjectTechnologies)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Technology)
            .WithMany(x => x.ProjectTechnologies)
            .HasForeignKey(x => x.TechnologyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.TechnologyId);
    }
}

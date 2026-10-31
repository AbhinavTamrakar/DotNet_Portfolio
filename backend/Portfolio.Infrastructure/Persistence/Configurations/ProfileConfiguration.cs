using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence.Configurations;

internal sealed class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("Profiles");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName).HasMaxLength(150).IsRequired();
        builder.Property(x => x.ProfessionalTitle).HasMaxLength(200).IsRequired();
        builder.Property(x => x.ShortBio).HasColumnType("text").IsRequired();
        builder.Property(x => x.Biography).HasColumnType("longtext").IsRequired();
        builder.Property(x => x.Email).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Location).HasMaxLength(150).IsRequired();
        builder.Property(x => x.ProfileImageUrl).HasMaxLength(1000).IsRequired();
        builder.Property(x => x.CvFileUrl).HasMaxLength(1000).IsRequired();

        builder.Property(x => x.CreatedAt).HasColumnType("datetime(6)").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnType("datetime(6)").IsRequired();
    }
}

internal sealed class SocialLinkConfiguration : IEntityTypeConfiguration<SocialLink>
{
    public void Configure(EntityTypeBuilder<SocialLink> builder)
    {
        builder.ToTable("SocialLinks");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Platform).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Url).HasMaxLength(1000).IsRequired();

        builder.HasIndex(x => x.DisplayOrder);
    }
}

internal sealed class SiteSettingConfiguration : IEntityTypeConfiguration<SiteSetting>
{
    public void Configure(EntityTypeBuilder<SiteSetting> builder)
    {
        builder.ToTable("SiteSettings");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.SettingKey).HasMaxLength(150).IsRequired();
        builder.Property(x => x.SettingValue).HasColumnType("text");

        builder.Property(x => x.UpdatedAt).HasColumnType("datetime(6)").IsRequired();

        builder.HasIndex(x => x.SettingKey).IsUnique();
    }
}

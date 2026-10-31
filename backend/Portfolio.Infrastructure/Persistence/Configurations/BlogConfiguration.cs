using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence.Configurations;

internal sealed class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.ToTable("Blogs");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).HasMaxLength(250).IsRequired();
        builder.Property(x => x.Slug).HasMaxLength(270).IsRequired();
        builder.Property(x => x.Excerpt).HasColumnType("text").IsRequired();
        builder.Property(x => x.Content).HasColumnType("longtext").IsRequired();
        builder.Property(x => x.FeaturedImageUrl).HasMaxLength(1000).IsRequired();
        builder.Property(x => x.SeoTitle).HasMaxLength(250);
        builder.Property(x => x.SeoDescription).HasMaxLength(500);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.PublishedAt).HasColumnType("datetime(6)");
        builder.Property(x => x.CreatedAt).HasColumnType("datetime(6)").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnType("datetime(6)").IsRequired();

        builder.HasIndex(x => x.Slug).IsUnique();
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.PublishedAt);
    }
}

internal sealed class BlogCategoryConfiguration : IEntityTypeConfiguration<BlogCategory>
{
    public void Configure(EntityTypeBuilder<BlogCategory> builder)
    {
        builder.ToTable("BlogCategories");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(120).IsRequired();
        builder.Property(x => x.Slug).HasMaxLength(140).IsRequired();

        builder.HasIndex(x => x.Slug).IsUnique();
    }
}

internal sealed class BlogTagConfiguration : IEntityTypeConfiguration<BlogTag>
{
    public void Configure(EntityTypeBuilder<BlogTag> builder)
    {
        builder.ToTable("BlogTags");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(120).IsRequired();
        builder.Property(x => x.Slug).HasMaxLength(140).IsRequired();

        builder.HasIndex(x => x.Slug).IsUnique();
    }
}

internal sealed class BlogPostCategoryConfiguration : IEntityTypeConfiguration<BlogPostCategory>
{
    public void Configure(EntityTypeBuilder<BlogPostCategory> builder)
    {
        builder.ToTable("BlogPostCategories");
        builder.HasKey(x => new { x.BlogId, x.CategoryId });

        builder.HasOne(x => x.Blog)
            .WithMany(x => x.Categories)
            .HasForeignKey(x => x.BlogId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.CategoryId);
    }
}

internal sealed class BlogPostTagConfiguration : IEntityTypeConfiguration<BlogPostTag>
{
    public void Configure(EntityTypeBuilder<BlogPostTag> builder)
    {
        builder.ToTable("BlogPostTags");
        builder.HasKey(x => new { x.BlogId, x.TagId });

        builder.HasOne(x => x.Blog)
            .WithMany(x => x.Tags)
            .HasForeignKey(x => x.BlogId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Tag)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.TagId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.TagId);
    }
}

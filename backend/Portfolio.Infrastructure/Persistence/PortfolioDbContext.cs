using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Common;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Identity;
using Portfolio.Infrastructure.Persistence.Converters;

namespace Portfolio.Infrastructure.Persistence;

/// <summary>
/// Application database context. Contains the ASP.NET Core Identity tables and all
/// portfolio content tables. Every timestamp is stored in UTC.
/// </summary>
public class PortfolioDbContext : IdentityDbContext<ApplicationUser>
{
    public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
        : base(options)
    {
    }

    public DbSet<Profile> Profiles => Set<Profile>();

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<ProjectImage> ProjectImages => Set<ProjectImage>();

    public DbSet<Technology> Technologies => Set<Technology>();

    public DbSet<ProjectTechnology> ProjectTechnologies => Set<ProjectTechnology>();

    public DbSet<Skill> Skills => Set<Skill>();

    public DbSet<Experience> Experiences => Set<Experience>();

    public DbSet<Education> Educations => Set<Education>();

    public DbSet<Certification> Certifications => Set<Certification>();

    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();

    public DbSet<BlogCategory> BlogCategories => Set<BlogCategory>();

    public DbSet<BlogTag> BlogTags => Set<BlogTag>();

    public DbSet<BlogPostCategory> BlogPostCategories => Set<BlogPostCategory>();

    public DbSet<BlogPostTag> BlogPostTags => Set<BlogPostTag>();

    public DbSet<Inquiry> Inquiries => Set<Inquiry>();

    public DbSet<SocialLink> SocialLinks => Set<SocialLink>();

    public DbSet<SiteSetting> SiteSettings => Set<SiteSetting>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(PortfolioDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // Applied to both DateTime and DateTime? properties.
        configurationBuilder.Properties<DateTime>().HaveConversion<UtcDateTimeConverter>();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ApplyAuditTimestamps();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        ApplyAuditTimestamps();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void ApplyAuditTimestamps()
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (entry.Entity is ICreatedAtEntity created && created.CreatedAt == default)
                    {
                        created.CreatedAt = now;
                    }

                    if (entry.Entity is IUpdatedAtEntity updated)
                    {
                        updated.UpdatedAt = now;
                    }

                    break;

                case EntityState.Modified:
                    // Creation timestamps are immutable once the row exists.
                    if (entry.Entity is ICreatedAtEntity)
                    {
                        entry.Property(nameof(ICreatedAtEntity.CreatedAt)).IsModified = false;
                    }

                    if (entry.Entity is IUpdatedAtEntity updatedEntity)
                    {
                        updatedEntity.UpdatedAt = now;
                        entry.Property(nameof(IUpdatedAtEntity.UpdatedAt)).IsModified = true;
                    }

                    break;
            }
        }
    }
}

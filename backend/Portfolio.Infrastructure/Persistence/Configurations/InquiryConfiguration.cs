using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence.Configurations;

internal sealed class InquiryConfiguration : IEntityTypeConfiguration<Inquiry>
{
    public void Configure(EntityTypeBuilder<Inquiry> builder)
    {
        builder.ToTable("Inquiries");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(150).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Subject).HasMaxLength(250).IsRequired();
        builder.Property(x => x.Message).HasColumnType("text").IsRequired();

        builder.Property(x => x.CreatedAt).HasColumnType("datetime(6)").IsRequired();

        builder.HasIndex(x => x.IsRead);
        builder.HasIndex(x => x.CreatedAt);
    }
}

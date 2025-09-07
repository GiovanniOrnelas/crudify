using Crudify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Crudify.Infrastructure.EF.Configurations
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("tenant");

            builder.HasKey(tenant => tenant.Id);

            builder.Property(tenant => tenant.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(tenant => tenant.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(tenant => tenant.CreatedAt)
                .HasColumnName("createdAt")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(tenant => tenant.UpdatedAt)
                .HasColumnName("updatedAt")
                .HasConversion(new ValueConverter<DateTime, DateTime>(
                        v => v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                )
                .IsRequired();

            builder.Property(tenant => tenant.Active)
                .HasColumnName("active")
                .HasDefaultValue(true)
                .IsRequired();

            builder.HasData(new
            {
                Id = 1L,
                Name = "Crudify",
                CreatedAt = new DateTime(2025, 09, 03),
                UpdatedAt = new DateTime(2025, 09, 03),
                Active = true
            });
        }
    }
}
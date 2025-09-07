using Crudify.Commons.Enums;
using Crudify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;

namespace Crudify.Infrastructure.EF.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(user => user.Email)
                .HasColumnName("email")
                .IsRequired();

            builder.Property(user => user.Type)
                .HasColumnName("type")
                .HasConversion(new EnumToStringConverter<UserType>())
                .IsRequired();

            builder.Property(user => user.Password)
                .HasColumnName("password")
                .IsRequired();

            builder.Property(user => user.Document)
                .HasColumnName("document")
                .IsRequired();

            builder.Property(user => user.Birthdate)
                .HasColumnName("birthdate")
                .HasConversion(new ValueConverter<DateTime, DateTime>(
                        v => v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                )
                .IsRequired();

            builder.Property(user => user.Gender)
                .HasColumnName("gender")
                .HasConversion(new EnumToStringConverter<Gender>())
                .IsRequired();

            builder.Property(user => user.CreatedAt)
                .HasColumnName("createdAt")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(user => user.UpdatedAt)
                .HasColumnName("updatedAt")
                .HasConversion(new ValueConverter<DateTime, DateTime>(
                        v => v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                )
                .IsRequired();

            builder.Property(user => user.Active)
                .HasColumnName("active")
                .HasDefaultValue(true)
                .IsRequired();

            builder.HasOne(user => user.Tenant)
                   .WithOne()
                   .HasForeignKey<User>(user => user.TenantId)
                   .IsRequired();

            builder.HasData(new
            {
                Id = 1L,
                TenantId = 1L,
                Name = "Crudify Admin",
                Email = "admin@crudify.com",
                Password = "admin123",
                Document = "111.111.111-11",
                Birthdate = new DateTime(1990, 01, 01),
                Type = UserType.Management,
                Gender = Gender.Male,
                CreatedAt = new DateTime(2025, 09, 03),
                UpdatedAt = new DateTime(2025, 09, 03),
                Active = true
            });
        }
    }
}
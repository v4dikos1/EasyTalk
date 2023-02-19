using EasyTalks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTalk.Persistence.EntityTypeConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).HasMaxLength(20).IsRequired();
            builder.HasIndex(r => r.Name).IsUnique();

            builder
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}

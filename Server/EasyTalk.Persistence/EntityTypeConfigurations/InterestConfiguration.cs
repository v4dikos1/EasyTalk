using EasyTalks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTalk.Persistence.EntityTypeConfigurations
{
    public class InterestConfiguration : IEntityTypeConfiguration<Interest>
    {
        public void Configure(EntityTypeBuilder<Interest> builder)
        {
            builder.ToTable("Inerest");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).HasMaxLength(15).IsRequired();
            builder.HasIndex(i => i.Name).IsUnique();

            builder
                .HasMany(i => i.Users)
                .WithMany(u => u.Interests);
        }
    }
}

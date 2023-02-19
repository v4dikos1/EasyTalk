using EasyTalks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTalk.Persistence.EntityTypeConfigurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable("Language");

            builder.HasKey(l => l.Id);
            builder.Property(l => l.Name).HasMaxLength(25).IsRequired();
            builder.HasIndex(l => l.Name).IsUnique();

            builder
                .HasMany(l => l.Users)
                .WithMany(u => u.TargetLanguages);
        }
    }
}

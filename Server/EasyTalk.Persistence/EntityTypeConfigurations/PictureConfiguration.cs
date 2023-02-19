using EasyTalks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTalk.Persistence.EntityTypeConfigurations
{
    public class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.ToTable("Picture");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Path).IsRequired();

            builder
                .HasOne(p => p.User)
                .WithOne(u => u.Picture)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

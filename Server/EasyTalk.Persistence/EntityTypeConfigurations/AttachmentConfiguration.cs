using EasyTalks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTalk.Persistence.EntityTypeConfigurations
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable("Attachment");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Label).HasMaxLength(250).IsRequired();
            builder.Property(a => a.Path).IsRequired();

            builder
                .HasOne(a => a.Message)
                .WithMany(m => m.Attachments)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using EasyTalks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTalk.Persistence.EntityTypeConfigurations
{
    internal class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Message");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Content).HasMaxLength(800);
            builder.Property(m => m.Date).IsRequired();
            builder.Property(m => m.IsRead).IsRequired();

            builder
                .HasOne(m => m.Sender);

            builder
                .HasMany(m => m.Attachments)
                .WithOne(a => a.Message);
        }
    }
}

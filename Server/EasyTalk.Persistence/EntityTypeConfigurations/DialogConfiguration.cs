﻿using EasyTalks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTalk.Persistence.EntityTypeConfigurations
{
    public class DialogConfiguration : IEntityTypeConfiguration<Dialog>
    {
        public void Configure(EntityTypeBuilder<Dialog> builder)
        {
            builder.ToTable("Dialog");

            builder.HasKey(d => d.Id);
            builder.Property(d => d.Title).HasMaxLength(20).IsRequired();

            builder
                .HasMany(d => d.Users)
                .WithMany(u => u.Dialogs);
        }
    }
}
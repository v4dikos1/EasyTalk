﻿using EasyTalks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTalk.Persistence.EntityTypeConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Firstname).IsRequired().HasMaxLength(15);
            builder.Property(u => u.Lastname).IsRequired().HasMaxLength(20);
            builder.Property(u => u.Patronymic).HasMaxLength(20);

            builder.Property(u => u.Info).IsRequired().HasMaxLength(150);

            builder.Property(u => u.Username).IsRequired().HasMaxLength(20);
            builder.HasIndex(u => u.Username).IsUnique();

            builder.Property(u => u.Email).IsRequired();
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.PhoneNumber).IsRequired().HasColumnType("char(11)");
            builder.HasIndex(u => u.PhoneNumber).IsUnique();

            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.PasswordSalt).IsRequired();

            builder.Property(u => u.NativeLanguage).IsRequired();
            builder
                .HasOne(u => u.NativeLanguage);
                

            builder.Property(u => u.TargetLanguages).IsRequired();
            builder
                .HasMany(u => u.TargetLanguages)
                .WithMany(l => l.Users);

            builder.Property(u => u.Interests).IsRequired();
            builder
                .HasMany(u => u.Interests)
                .WithMany(i => i.Users);

            builder.Property(u => u.Role).IsRequired();
            builder
                .HasOne(u => u.Role)
                .WithMany(r => r.Users);

            builder.Property(u => u.Picture).IsRequired();
            builder
                .HasOne(u => u.Picture)
                .WithOne(p => p.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(u => u.Dialogs)
                .WithMany(d => d.Users);
        }   
    }
}

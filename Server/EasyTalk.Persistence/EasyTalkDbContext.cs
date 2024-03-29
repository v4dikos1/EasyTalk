﻿using EasyTalk.Application.Interfaces;
using EasyTalk.Persistence.EntityTypeConfigurations;
using EasyTalks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyTalk.Persistence
{
    public class EasyTalkDbContext : DbContext, IEasyTalkDbContext
    {
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<User> Users { get; set; }

        public EasyTalkDbContext(DbContextOptions<EasyTalkDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AttachmentConfiguration());
            modelBuilder.ApplyConfiguration(new DialogConfiguration());
            modelBuilder.ApplyConfiguration(new InterestConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new PictureConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<Language>().HasData(
                new Language { Code = "ru" },
                new Language { Code = "en" },
                new Language { Code = "cs" });

            modelBuilder.Entity<Interest>().HasData(
                new Interest { Name = "reading" },
                new Interest { Name = "sport" },
                new Interest { Name = "traveling" });
        }
    }
}

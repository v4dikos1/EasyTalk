using EasyTalks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyTalk.Application.Interfaces
{
    public interface IEasyTalkDbContext
    {
        DbSet<Attachment> Attachments { get; set; }
        DbSet<Dialog> Dialogs { get; set; }
        DbSet<Interest> Interests { get; set; }
        DbSet<Language> Languages { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<Picture> Pictures { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

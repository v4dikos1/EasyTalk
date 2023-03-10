using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalk.Application.Users.Queries;
using EasyTalks.Domain.Entities;

namespace EasyTalk.Application.Dialogs.Queries
{
    public class DialogLookupDto : IMapWith<Dialog>
    {
        /// <summary>
        /// Id диалога
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Участники диалога
        /// </summary>
        public List<UserProfileVm> Users { get; set; } = new();

        /// <summary>
        /// Сообщения
        /// </summary>
        public List<Message> Messages { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Dialog, DialogLookupDto>();
        }
    }
}

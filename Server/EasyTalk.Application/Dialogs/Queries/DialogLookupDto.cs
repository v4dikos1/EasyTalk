using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalk.Application.Dialogs.Queries.GetDialogsList;
using EasyTalk.Application.Messages;
using EasyTalks.Domain.Entities;

namespace EasyTalk.Application.Dialogs.Queries
{
    public class DialogLookupDto : IMapWith<Dialog>
    {
        /// <summary>
        /// Code диалога
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Участники диалога
        /// </summary>
        public List<UserDialogVm> Users { get; set; } = new();

        /// <summary>
        /// Сообщения
        /// </summary>
        public List<MessageDialogViewModel> Messages { get; set; } = new();

        /// <summary>
        /// Вложения диалога
        /// </summary>
        public List<Guid> Attachments {get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Dialog, DialogLookupDto>();
        }
    }
}

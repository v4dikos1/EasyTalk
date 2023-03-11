using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalk.Application.Dialogs.Commands.CreateDialog;

namespace EasyTalk.WebApi.Models.Chat
{
    public class CreateDialogDto : IMapWith<CreateDialogCommand>
    {
        /// <summary>
        /// Участники диалога
        /// </summary>
        public List<Guid> Users { get; set; } = null!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateDialogDto, CreateDialogCommand>();
        }
    }
}

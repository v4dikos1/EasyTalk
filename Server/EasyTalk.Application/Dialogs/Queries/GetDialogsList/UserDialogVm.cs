using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalks.Domain.Entities;

namespace EasyTalk.Application.Dialogs.Queries.GetDialogsList
{
    /// <summary>
    /// Модель данных для отображения списка диалогов пользователя
    /// </summary>
    public class UserDialogVm : IMapWith<User>
    {
        /// <summary>
        /// Code пользователя, участвующего в диалоге
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Юзернейм пользователя, участвующего в диалоге
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Аватар пользователя, участвующего в диалоге
        /// </summary>
        public Guid? PictureId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDialogVm>();
        }
    }
}

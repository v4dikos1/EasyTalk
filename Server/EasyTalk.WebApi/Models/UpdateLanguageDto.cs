using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalk.Application.Languages.Commands.UpdateLanguage;

namespace EasyTalk.WebApi.Models
{
    /// <summary>
    /// Обновление языка
    /// </summary>
    public class UpdateLanguageDto : IMapWith<UpdateLanguageCommand>
    {
        /// <summary>
        /// Id изменяемого языка
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Новое название
        /// </summary>
        public string NewName { get; set; } = String.Empty;

        public void Mapping (Profile profile)
        {
            profile.CreateMap<UpdateLanguageDto, UpdateLanguageCommand>();
        }
    }
}

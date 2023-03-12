using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EasyTalk.Application.Languages.Queries.GetLanguagesList
{
    /// <summary>
    /// Получение списка языков
    /// </summary>
    public class GetLanguagesListQuery : IRequest<LanguageListVm>
    {
        /// <summary>
        /// Смещение
        /// </summary>
        [Required]
        public int Offset { get; set; }

        /// <summary>
        /// Лимит
        /// </summary>
        [Required]
        public int Limit { get; set; }
    }
}

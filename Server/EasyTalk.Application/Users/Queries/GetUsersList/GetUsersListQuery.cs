using MediatR;

namespace EasyTalk.Application.Users.Queries.GetUsersList
{
    public class GetUsersListQuery : IRequest<UsersListVm>
    {
        /// <summary>
        /// Ограничение по количеству возвращаемых пользователей
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Отступ от начала
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Фильтр по интересам
        /// </summary>
        public List<Guid>? InterestsFilter { get; set; }

        /// <summary>
        /// Фильтр по родному языку
        /// </summary>
        public List<Guid>? NativeLanguagesFilter { get; set; }

        /// <summary>
        /// Фильтр по изучаемым языкам
        /// </summary>
        public List<Guid>? TargetLanguagesFilter { get; set; }
    }
}

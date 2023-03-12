namespace EasyTalk.WebApi.Models.User
{
    public class GetUsersListRequest
    {
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

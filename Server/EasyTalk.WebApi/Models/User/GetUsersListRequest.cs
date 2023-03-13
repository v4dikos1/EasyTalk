namespace EasyTalk.WebApi.Models.User
{
    public class GetUsersListRequest
    {
        /// <summary>
        /// Фильтр по интересам
        /// </summary>
        public List<string>? InterestsFilter { get; set; }

        /// <summary>
        /// Фильтр по родному языку
        /// </summary>
        public List<string>? NativeLanguagesFilter { get; set; }

        /// <summary>
        /// Фильтр по изучаемым языкам
        /// </summary>
        public List<string>? TargetLanguagesFilter { get; set; }
    }
}

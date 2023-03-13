namespace EasyTalk.WebApi.Models.Chat
{
    public class TranslateRequestDto
    {
        /// <summary>
        /// Текст для перевода
        /// </summary>
        public string Text {get; set; } = string.Empty;

        /// <summary>
        /// Язык, на который необходимо перевести
        /// </summary>
        public string TargetLanguageCode { get; set; } = string.Empty;
    }
}

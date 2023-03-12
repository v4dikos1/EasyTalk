namespace EasyTalk.Application.Languages.Queries.GetLanguagesList
{
    /// <summary>
    /// Список языков
    /// </summary>
    public class LanguageListVm
    {
        public IList<LanguageLookupDto> Languages { get; set; } = null!;

    }
}

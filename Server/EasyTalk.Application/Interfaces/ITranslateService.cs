namespace EasyTalk.Application.Interfaces
{
    public interface ITranslateService
    {
        Task<string> TranslateAsync(string text, string targetLanguageCode);
    }
}

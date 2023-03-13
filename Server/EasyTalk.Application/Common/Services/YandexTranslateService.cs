using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace EasyTalk.Application.Common.Services
{
    public class YandexTranslateService : ITranslateService
    {
        private readonly IConfiguration _configuration;
        private const string URL = "https://translate.api.cloud.yandex.net/translate/v2/";

        public YandexTranslateService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> TranslateAsync(string text, string targetLanguageCode)
        {
            try
            {
                var token = _configuration.GetConnectionString("YandexApiKey");

                var responseString = await (URL + "translate")
                    .WithHeader("Authorization", $"Api-Key {token}")
                    .PostJsonAsync(new { targetLanguageCode = targetLanguageCode, texts = new List<string> { text } })
                    .ReceiveString();


                dynamic response = JObject.Parse(responseString);

                var result = response.translations[0].text;

                return result;
            }
            catch (FlurlHttpException ex)
            {
                throw new TranslateException(ex.Message);
            }


        }
    }
}

using Google.Apis.Auth.OAuth2;
using Google.Cloud.Translation.V2;

namespace TextTranslator.Library;

public class GoogleTranslate : ITranslate
{
    private readonly TranslationClient _client;

    public GoogleTranslate()
    {
        _client = TranslationClient.Create(GoogleCredential.FromFile("key.json"));
    }

    public async Task<string> TranslateText(string text, string language)
    {
        var result = await _client.TranslateTextAsync(text, language);

        return result.TranslatedText;
    }
}
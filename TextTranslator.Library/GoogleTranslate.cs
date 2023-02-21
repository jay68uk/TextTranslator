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

    public async Task<TranslationModel> TranslateText(string text, string language)
    {
        var result = await _client.TranslateTextAsync(text, language);

        return new TranslationModel
        {
            DetectedSourceLanguage = result.DetectedSourceLanguage,
            OriginalText = result.OriginalText,
            TargetLanguage = result.TargetLanguage,
            TranslatedText = result.TranslatedText
        };
    }
}
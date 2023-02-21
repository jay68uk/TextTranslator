namespace TextTranslator.Library;

public record TranslationModel
{
    public string? OriginalText { get; init; }

    public string? TranslatedText { get; init; }

    public string? DetectedSourceLanguage { get; init; }

    public string? TargetLanguage { get; init; }
}
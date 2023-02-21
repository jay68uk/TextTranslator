namespace TextTranslator.Library;

public interface ITranslate
{
    Task<TranslationModel> TranslateText(string text, string language);
}
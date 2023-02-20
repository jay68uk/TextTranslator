namespace TextTranslator.Library;

public interface ITranslate
{
    Task<string> TranslateText(string text, string language);
}
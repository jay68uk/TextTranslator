namespace TextTranslator.Library;

public interface ITextServices
{
    Task<TranslationModel> ProcessTextInput(string text, string language);
}
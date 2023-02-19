namespace TextTranslator.Library;

public interface ITextServices
{
    string ProcessTextInput(string text, string language);
    string CheckSourceLanguage(string input);
    string LoadTextFile(string path, string language);
}
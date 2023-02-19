using Throw;

namespace TextTranslator.Library;

public class TextServices : ITextServices
{
    public string ProcessTextInput(string text, string language)
    {
        string check;

        try
        {
            check = ValidateText(text);
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine(e);
            throw;
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine(e);
            check = text[..128];
        }

        return check;
    }

    public string CheckSourceLanguage(string input)
    {
        throw new NotImplementedException();
    }

    public string LoadTextFile(string path, string language)
    {
        throw new NotImplementedException();
    }


    private static string ValidateText(string input)
    {
        input.Throw(() => new ArgumentNullException(nameof(input), "No text to translate!"))
            .IfTrue(string.IsNullOrEmpty(input));

        input.Throw(() => new ArgumentOutOfRangeException(nameof(input), "Text will be truncated to 128 characters!"))
            .IfTrue(input.Length > 128);

        return input;
    }
}
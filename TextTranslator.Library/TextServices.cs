using Throw;

namespace TextTranslator.Library;

public class TextServices : ITextServices
{
    private readonly ITranslate _service;

    public TextServices(ITranslate service)
    {
        _service = service;
    }

    public async Task<TranslationModel> ProcessTextInput(string text, string language)
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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.ResetColor();
            check = text[..128];
        }

        var translation = await _service.TranslateText(check, language);

        return translation;
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
using TextTranslator.Library;

namespace TextTranslator.Console;

internal class TranslationHandler
{
    private readonly ITextServices _textService;

    public TranslationHandler(ITextServices textService)
    {
        _textService = textService;
    }

    public void ProcessTranslationInput()
    {
        bool invalidInput;

        do
        {
            invalidInput = false;
            System.Console.WriteLine("Enter T (text input), F (text file) or X to exit.");
            var transType = System.Console.ReadKey();

            try
            {
                // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
                switch (transType.Key)
                {
                    case ConsoleKey.X:
                        return;
                    case ConsoleKey.F:

                        break;
                    case ConsoleKey.T:
                        var inputText = GetText();
                        var lang = GetLanguage();
                        System.Console.WriteLine("Language is {0}", lang);
                        _textService.ProcessTextInput(inputText!, "cy");
                        break;
                    default:
                        System.Console.WriteLine("Please enter X, F or T.");
                        invalidInput = true;
                        break;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine();
                System.Console.WriteLine("Problem processing the translation: " + e.Message);
                System.Console.WriteLine("Ctrl+C to quit or try again");
                invalidInput = true;
            }
        } while (invalidInput);
    }

    private static string? GetText()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("Enter the text to be translated:");
        var input = System.Console.ReadLine();

        return input;
    }

    private static string GetLanguage()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("Select language to translate to (X to exit):");
        foreach (var lang in Enum.GetValues(typeof(LanguageEnum)))
            System.Console.WriteLine((int)lang + " - " + (LanguageEnum)lang);

        bool invalidInput;
        var retVal = string.Empty;

        do
        {
            invalidInput = true;
            var transType = System.Console.ReadLine();

            if (transType.ToUpper() == "X")
            {
                retVal = "X";
                invalidInput = true;
            }

            var isNumber = int.TryParse(transType, out var selectedLang);

            if (!Enum.IsDefined(typeof(LanguageEnum), selectedLang) || !isNumber) continue;
            retVal = ((LanguageEnum)selectedLang).ToString();
            invalidInput = false;
        } while (invalidInput);

        return retVal;
    }
}
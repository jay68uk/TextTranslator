using TextTranslator.Library;

namespace TextTranslator.Console;

internal class TranslationHandler
{
    private readonly ITextServices _textService;

    public TranslationHandler(ITextServices textService)
    {
        _textService = textService;
    }

    public async Task ProcessTranslationInput()
    {
        bool validInput;

        do
        {
            validInput = true;
            System.Console.WriteLine();
            System.Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine("Enter T (text input), F (text file) or X to exit.");
            var transType = System.Console.ReadKey();

            try
            {
                // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
                switch (transType.Key)
                {
                    case ConsoleKey.X:
                        System.Console.ForegroundColor = ConsoleColor.White;
                        validInput = false;
                        break;
                    case ConsoleKey.F:
                        var fileText = GetTextFromFile();
                        await SetupTranslation(fileText);
                        break;
                    case ConsoleKey.T:
                        var inputText = GetText();
                        await SetupTranslation(inputText);
                        break;
                    default:
                        System.Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("Please enter X, F or T.");
                        break;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine();
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Problem processing the translation: " + e.Message);
                System.Console.WriteLine("Ctrl+C to quit or try again");
                validInput = false;
            }
        } while (validInput);
    }

    private async Task SetupTranslation(string? inputText)
    {
        var lang = GetLanguage();
        var translation = await _textService.ProcessTextInput(inputText!, lang);
        ShowResult(translation);
    }

    private static string? GetTextFromFile()
    {
        System.Console.WriteLine();
        System.Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.WriteLine("Enter the path of the text file to be translated:");
        var path = System.Console.ReadLine();

        var fileText = string.Empty;

        if (path == null) return fileText;

        try
        {
            var r = new StreamReader(path);
            fileText = r.ReadToEnd();
        }
        catch (Exception e)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(e);
            System.Console.ResetColor();
        }


        return fileText;
    }

    private static string? GetText()
    {
        System.Console.WriteLine();
        System.Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.WriteLine("Enter the text to be translated:");
        var input = System.Console.ReadLine();

        return input;
    }

    private static string GetLanguage()
    {
        System.Console.WriteLine();
        System.Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine("Select language to translate to (X to exit):");
        foreach (var lang in Enum.GetValues(typeof(LanguageEnum)))
            System.Console.WriteLine((int)lang + " - " + (LanguageEnum)lang);

        bool doLoop;
        var retVal = string.Empty;

        do
        {
            doLoop = true;
            var transType = System.Console.ReadLine();

            if (transType!.ToUpper() == "X")
            {
                retVal = "X";
                doLoop = true;
            }

            var isNumber = int.TryParse(transType, out var selectedLang);

            if (!Enum.IsDefined(typeof(LanguageEnum), selectedLang) || !isNumber) continue;
            retVal = ((LanguageEnum)selectedLang).ToString();
            doLoop = false;
        } while (doLoop);

        return retVal;
    }

    private static void ShowResult(TranslationModel result)
    {
        System.Console.WriteLine();
        System.Console.ForegroundColor = ConsoleColor.Cyan;
        System.Console.WriteLine("Translation from {0}:", result.DetectedSourceLanguage);
        System.Console.WriteLine("Translation into {0}:", result.TargetLanguage);
        System.Console.WriteLine();
        System.Console.ForegroundColor = ConsoleColor.DarkBlue;
        System.Console.BackgroundColor = ConsoleColor.DarkGray;
        System.Console.WriteLine("Translation result: {0}", result.TranslatedText);
        System.Console.ResetColor();
        System.Console.WriteLine();
    }
}
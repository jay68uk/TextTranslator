// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using TextTranslator.Console;
using TextTranslator.Library;

var host = Startup.CreateHostBuilder(args).Build();

Console.WriteLine("Translates input text or text from a txt file to the selected language.");
Console.WriteLine("It will try and detect the language of the text to be translated.");
Console.WriteLine("Translation is done via the Google Translate API.");

var textService = host.Services.GetRequiredService<ITextServices>();
var app = new TranslationHandler(textService);
app.ProcessTranslationInput();
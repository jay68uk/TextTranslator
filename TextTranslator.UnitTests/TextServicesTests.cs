using TextTranslator.Library;

namespace TextTranslator.UnitTests;

public class TextServicesTests
{
    private const string Input = "the quick brown fox jumped over the lazy dog.";
    private const string OutputEn = Input;
    private const string InputCy = "neidiodd y llwynog brown cyflym dros y ci diog.";
    private const string OutputCy = InputCy;
    private const string OutputFr = "Le rapide renard brun sauta par dessus le chien paresseux.";

    private readonly TextServices _service = new(new GoogleTranslate());

    [Fact]
    public async Task ProcessTextInputTest_Success_En_Cy()
    {
        var result = await _service.ProcessTextInput(Input, "cy");
        Assert.Equal(OutputCy, result);
    }

    [Fact]
    public async Task ProcessTextInputTest_Success_En_Fr()
    {
        var result = await _service.ProcessTextInput(Input, "fr");
        Assert.Equal(OutputFr, result);
    }

    [Fact]
    public async Task ProcessTextInputTest_Success_Cy_En()
    {
        var result = await _service.ProcessTextInput(InputCy, "en");
        Assert.Equal(OutputEn, result);
    }

    [Fact]
    public async Task ProcessTextInputTest_Truncate()
    {
        var input = Input + Environment.NewLine + Input + Environment.NewLine + Input;

        var result = await _service.ProcessTextInput(input, "cy");
        Assert.Equal(input[..128], result);
    }

    [Fact]
    public void ProcessTextInputTest_Empty()
    {
        Assert.ThrowsAsync<ArgumentNullException>(() => _service.ProcessTextInput("", "cy"));
    }
}
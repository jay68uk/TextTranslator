using TextTranslator.Library;

namespace TextTranslator.UnitTests;

public class TextServicesTests
{
    private const string _input = "the quick brown fox jumper over the lazy dog.";
    private const string _inputCy = "y siwmper llwynog brown cyflym dros y ci diog.";
    private readonly string _outputCy = _inputCy;
    private readonly string _outputFr = "le cavalier rapide du renard brun sur le chien paresseux.";

    private readonly TextServices _service = new();

    [Fact]
    public void ProcessTextInputTest_Success_En_Cy()
    {
        var result = _service.ProcessTextInput(_input, "cy");
        Assert.Equal(_outputCy, result);
    }

    [Fact]
    public void ProcessTextInputTest_Truncate()
    {
        var input = _input + Environment.NewLine + _input + Environment.NewLine + _input;

        var result = _service.ProcessTextInput(input, "cy");
        Assert.Equal(input[..128], result);
    }

    [Fact]
    public void ProcessTextInputTest_Empty()
    {
        Assert.Throws<ArgumentNullException>(() => _service.ProcessTextInput("", "cy"));
    }
}
namespace Blism.Language.Dart;

public class DartHighlighterLanguage : IHighlighterLanguage<DartTokenType>
{
	public static readonly DartHighlighterLanguage Instance = new();

	public ITokenizer<DartTokenType> Tokenizer => new DartTokenizer();

	public ITokenTypeHighlighter<DartTokenType> TypeHighlighter => new DartDarkHighlighter();
}

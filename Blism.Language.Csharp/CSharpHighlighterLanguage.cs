namespace Blism.Language.Csharp;

public class CSharpHighlighterLanguage : IHighlighterLanguage<CSharpTokenType>
{
	public static readonly CSharpHighlighterLanguage Instance = new();

	public ITokenizer<CSharpTokenType> Tokenizer => new CSharpTokenizer();

	public ITokenTypeHighlighter<CSharpTokenType> TypeHighlighter => new CSharpDarkHighlighter();
}

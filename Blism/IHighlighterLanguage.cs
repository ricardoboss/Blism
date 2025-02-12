namespace Blism;

public interface IHighlighterLanguage<TTokenType> where TTokenType : Enum
{
	public ITokenizer<TTokenType> Tokenizer { get; }

	public ITokenTypeHighlighter<TTokenType> TypeHighlighter { get; }
}

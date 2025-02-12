namespace Blism;

public interface ITokenizer<TTokenType> where TTokenType : Enum
{
	IEnumerable<SyntaxToken<TTokenType>> Tokenize(string code);
}

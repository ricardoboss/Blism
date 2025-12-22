namespace Blism.Core;

public interface ITokenTypeHighlighter<in TTokenType> where TTokenType : Enum
{
	string GetCss(TTokenType tokenType);

	string GetDefaultCss();
}

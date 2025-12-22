namespace Blism.Core;

public interface ITheme
{
	TokenStyle? GetTokenStyle(StyleTokenType tokenType);

	TokenStyle GetDefaultStyle();
}

namespace Blism.Test;

public class SimpleHighlighter : ITokenTypeHighlighter<SimpleTokenType>
{
	public string GetCss(SimpleTokenType tokenType)
	{
		return tokenType switch
		{
			SimpleTokenType.Char => "color: blue;",
			SimpleTokenType.Digit => "color: green;",
			SimpleTokenType.Keyword => "color: purple;",
			SimpleTokenType.Unknown => "color: yellow;",
			_ => "",
		};
	}

	public string GetDefaultCss()
	{
		return "color: red;";
	}
}

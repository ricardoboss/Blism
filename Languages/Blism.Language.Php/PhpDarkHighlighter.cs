namespace Blism.Language.Php;

public class PhpDarkHighlighter : ITokenTypeHighlighter<PhpTokenType>
{
	public static readonly PhpDarkHighlighter Instance = new();

	public virtual string GetCss(PhpTokenType tokenType)
	{
		return tokenType switch
		{
			PhpTokenType.Keyword => "color: #47a2ed;",
			PhpTokenType.String => "color: #cc884e;",
			PhpTokenType.Number => "color: #b3cca4;",
			PhpTokenType.Comment => "color: #699856; font-style: italic;",
			PhpTokenType.Identifier => "color: #47ccb1;",
			PhpTokenType.Variable => "color: #94dbfd;",
			_ => "",
		};
	}

	public virtual string GetDefaultCss()
	{
		return "color: #d4d4d4; background-color: #1e1e1e;";
	}
}

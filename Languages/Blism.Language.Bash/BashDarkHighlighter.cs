namespace Blism.Language.Bash;

public class BashDarkHighlighter : ITokenTypeHighlighter<BashTokenType>
{
	public static readonly BashDarkHighlighter Instance = new();

	public virtual string GetCss(BashTokenType tokenType)
	{
		return tokenType switch
		{
			BashTokenType.Keyword => "color: #47a2ed; font-weight: bold;",
			BashTokenType.String => "color: #cc884e;",
			BashTokenType.Comment => "color: #699856; font-style: italic;",
			BashTokenType.SheBang => "color: #699856; font-style: italic; font-weight: bold;",
			BashTokenType.Number => "color: #b3cca4;",
			BashTokenType.Identifier => "color: #94dbfd;",
			BashTokenType.Command => "color: #47a2ed;",
			_ => "",
		};
	}

	public virtual string GetDefaultCss()
	{
		return "color: #d4d4d4; background-color: #1e1e1e;";
	}
}

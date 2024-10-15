namespace Blism.Language.Csharp;

public class CSharpDarkHighlighter : ITokenTypeHighlighter<CSharpTokenType>
{
	public string GetCss(CSharpTokenType tokenType)
	{
		return tokenType switch
		{
			CSharpTokenType.Keyword => "color: #47a2ed; font-weight: bold;",
			CSharpTokenType.String => "color: #cc884e;",
			CSharpTokenType.Comment => "color: #699856; font-style: italic;",
			CSharpTokenType.Number => "color: #b3cca4;",
			CSharpTokenType.Identifier => "color: #94dbfd;",
			_ => "",
		};
	}

	public string GetDefaultCss()
	{
		return "color: #d4d4d4; background-color: #1e1e1e;";
	}
}

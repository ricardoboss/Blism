using Blism.Core;

namespace Blism.Language.CSharp.Core;

public class CSharpDarkHighlighter : ITokenTypeHighlighter<CSharpTokenType>
{
	public static readonly CSharpDarkHighlighter Instance = new();

	public virtual string GetCss(CSharpTokenType tokenType)
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

	public virtual string GetDefaultCss()
	{
		return "color: #d4d4d4; background-color: #1e1e1e;";
	}
}

using Blism.Core;

namespace Blism.Language.Dart.Core;

public class DartDarkHighlighter : ITokenTypeHighlighter<DartTokenType>
{
	public static readonly DartDarkHighlighter Instance = new();

	public virtual string GetCss(DartTokenType tokenType)
	{
		return tokenType switch
		{
			DartTokenType.Keyword => "color: #47a2ed; font-weight: bold;",
			DartTokenType.String => "color: #cc884e;",
			DartTokenType.Comment => "color: #699856; font-style: italic;",
			DartTokenType.Number => "color: #b3cca4;",
			DartTokenType.Identifier => "color: #94dbfd;",
			_ => "",
		};
	}

	public virtual string GetDefaultCss()
	{
		return "color: #d4d4d4; background-color: #1e1e1e;";
	}
}

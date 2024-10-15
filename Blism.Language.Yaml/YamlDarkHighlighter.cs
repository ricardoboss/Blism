namespace Blism.Language.Yaml;

public class YamlDarkHighlighter : ITokenTypeHighlighter<YamlTokenType>
{
	public string GetCss(YamlTokenType tokenType)
	{
		return tokenType switch
		{
			YamlTokenType.Comment => "color: #699856; font-style: italic;",
			YamlTokenType.Key => "color: #94dbfd;",
			YamlTokenType.Scalar => "color: #cc884e;",
			_ => "",
		};
	}

	public string GetDefaultCss()
	{
		return "color: #d4d4d4; background-color: #1e1e1e;";
	}
}

namespace Blism.Language.Yaml;

public class YamlHighlighterLanguage : IHighlighterLanguage<YamlTokenType>
{
	public static readonly YamlHighlighterLanguage Instance = new();

	public ITokenizer<YamlTokenType> Tokenizer => new YamlTokenizer();

	public ITokenTypeHighlighter<YamlTokenType> TypeHighlighter => new YamlDarkHighlighter();
}

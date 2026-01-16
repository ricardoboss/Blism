using Blism.Language.Yaml.Core;
using Blism.Wpf;

namespace Blism.Language.Yaml.Wpf;

public class YamlSyntaxHighlighter : SyntaxHighlighter<YamlTokenType>
{
	public YamlSyntaxHighlighter()
	{
		Tokenizer = YamlTokenizer.Instance;
		StyleMapper = YamlTokenStyleMapper.Instance;
	}
}

using Blism.Language.Php.Core;
using Blism.Wpf;

namespace Blism.Language.Php.Wpf;

public class PhpSyntaxHighlighter : SyntaxHighlighter<PhpTokenType>
{
	public PhpSyntaxHighlighter()
	{
		Tokenizer = PhpTokenizer.Instance;
		StyleMapper = PhpTokenStyleMapper.Instance;
	}
}

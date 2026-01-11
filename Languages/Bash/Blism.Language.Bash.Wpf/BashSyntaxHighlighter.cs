using Blism.Language.Bash.Core;
using Blism.Wpf;

namespace Blism.Language.Bash.Wpf;

public class BashSyntaxHighlighter : SyntaxHighlighter<BashTokenType>
{
	public BashSyntaxHighlighter()
	{
		Tokenizer = BashTokenizer.Instance;
		StyleMapper = BashTokenStyleMapper.Instance;
	}
}

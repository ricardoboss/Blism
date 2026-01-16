using Blism.Language.Dart.Core;
using Blism.Wpf;

namespace Blism.Language.Dart.Wpf;

public class DartSyntaxHighlighter : SyntaxHighlighter<DartTokenType>
{
	public DartSyntaxHighlighter()
	{
		Tokenizer = DartTokenizer.Instance;
		StyleMapper = DartTokenStyleMapper.Instance;
	}
}

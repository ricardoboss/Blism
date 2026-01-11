using Blism.Language.CSharp.Core;
using Blism.Wpf;

namespace Blism.Language.CSharp.Wpf;

public class CSharpSyntaxHighlighter : SyntaxHighlighter<CSharpTokenType>
{
	public CSharpSyntaxHighlighter()
	{
		Tokenizer = CSharpTokenizer.Instance;
		StyleMapper = CSharpTokenStyleMapper.Instance;
	}
}

using Microsoft.AspNetCore.Components;

namespace Blism;

public abstract class SyntaxHighlighterBase<TTokenType> : ComponentBase where TTokenType : Enum
{
	[Parameter, EditorRequired]
	public required ITokenizer<TTokenType> Tokenizer { get; set; }

	[Parameter, EditorRequired]
	public required ITokenTypeHighlighter<TTokenType> Highlighter { get; set; }
}

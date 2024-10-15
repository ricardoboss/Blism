using Microsoft.AspNetCore.Components;

namespace Blism;

public abstract class SyntaxHighlighterBase<TLanguage, TTokenType> : ComponentBase
	where TLanguage : IHighlighterLanguage<TTokenType>
	where TTokenType : Enum
{
	[Parameter, EditorRequired]
	public required TLanguage Language { get; set; }
}

namespace Blism.Core;

public class SyntaxToken<TTokenType> where TTokenType : Enum
{
	public required string Value { get; init; }

	public required TTokenType Type { get; init; }

	public override string ToString() => $"<{Type}>{Value}";
}

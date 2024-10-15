namespace Blism;

public class SyntaxToken<TTokenType> where TTokenType : Enum
{
    public required string Value { get; init; }

    public required TTokenType Type { get; init; }
}

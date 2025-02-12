namespace Blism;

public class SyntaxTokenComparer<TTokenType> : IEqualityComparer<SyntaxToken<TTokenType>> where TTokenType : Enum
{
	public bool Equals(SyntaxToken<TTokenType>? x, SyntaxToken<TTokenType>? y)
	{
		if (ReferenceEquals(x, y)) return true;
		if (x is null) return false;
		if (y is null) return false;
		if (x.GetType() != y.GetType()) return false;
		return x.Value == y.Value && Equals(x.Type, y.Type);
	}

	public int GetHashCode(SyntaxToken<TTokenType> obj) => HashCode.Combine(obj.Value, obj.Type);
}

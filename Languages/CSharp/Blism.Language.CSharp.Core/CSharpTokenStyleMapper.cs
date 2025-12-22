using Blism.Core;

namespace Blism.Language.CSharp.Core;

public class CSharpTokenStyleMapper : ITokenStyleMapper<CSharpTokenType>
{
	public static readonly CSharpTokenStyleMapper Instance = new();

	public StyleTokenType MapTokenType(CSharpTokenType tokenType)
	{
		return tokenType switch
		{
			CSharpTokenType.Keyword => StyleTokenType.Keyword,
			CSharpTokenType.Punctuation => StyleTokenType.Punctuation,
			CSharpTokenType.Number => StyleTokenType.Number,
			CSharpTokenType.String => StyleTokenType.String,
			CSharpTokenType.Whitespace => StyleTokenType.Whitespace,
			CSharpTokenType.Comment => StyleTokenType.Comment,
			CSharpTokenType.Identifier => StyleTokenType.Identifier,
			_ => StyleTokenType.Unknown,
		};
	}
}

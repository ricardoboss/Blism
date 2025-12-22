using Blism.Core;

namespace Blism.Language.Bash.Core;

public class BashTokenStyleMapper : ITokenStyleMapper<BashTokenType>
{
	public static readonly BashTokenStyleMapper Instance = new();

	public StyleTokenType MapTokenType(BashTokenType tokenType)
	{
		return tokenType switch
		{
			BashTokenType.Punctuation => StyleTokenType.Punctuation,
			BashTokenType.Whitespace => StyleTokenType.Whitespace,
			BashTokenType.Comment => StyleTokenType.Comment,
			BashTokenType.String => StyleTokenType.String,
			BashTokenType.Number => StyleTokenType.Number,
			BashTokenType.Identifier => StyleTokenType.Identifier,
			BashTokenType.Keyword => StyleTokenType.Keyword,
			BashTokenType.SheBang => StyleTokenType.SpecialComment,
			BashTokenType.Command => StyleTokenType.Keyword,
			BashTokenType.PositionalParameter => StyleTokenType.SpecialIdentifier,
			_ => StyleTokenType.Unknown,
		};
	}
}

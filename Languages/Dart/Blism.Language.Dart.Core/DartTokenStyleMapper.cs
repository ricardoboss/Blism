using Blism.Core;

namespace Blism.Language.Dart.Core;

public class DartTokenStyleMapper : ITokenStyleMapper<DartTokenType>
{
	public static readonly DartTokenStyleMapper Instance = new();

	public StyleTokenType MapTokenType(DartTokenType tokenType)
	{
		return tokenType switch
		{
			DartTokenType.Comment => StyleTokenType.Comment,
			DartTokenType.String => StyleTokenType.String,
			DartTokenType.Number => StyleTokenType.Number,
			DartTokenType.Punctuation => StyleTokenType.Punctuation,
			DartTokenType.Whitespace => StyleTokenType.Whitespace,
			DartTokenType.Keyword => StyleTokenType.Keyword,
			DartTokenType.Identifier => StyleTokenType.Identifier,
			_ => StyleTokenType.Unknown,
		};
	}
}

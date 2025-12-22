using Blism.Core;

namespace Blism.Language.Php.Core;

public class PhpTokenStyleMapper : ITokenStyleMapper<PhpTokenType>
{
	public static readonly PhpTokenStyleMapper Instance = new();

	public StyleTokenType MapTokenType(PhpTokenType tokenType)
	{
		return tokenType switch
		{
			PhpTokenType.Whitespace => StyleTokenType.Whitespace,
			PhpTokenType.Keyword => StyleTokenType.Keyword,
			PhpTokenType.String => StyleTokenType.String,
			PhpTokenType.Number => StyleTokenType.Number,
			PhpTokenType.Comment => StyleTokenType.Comment,
			PhpTokenType.Type => StyleTokenType.Type,
			PhpTokenType.Identifier => StyleTokenType.Identifier,
			_ => StyleTokenType.Unknown,
		};
	}
}

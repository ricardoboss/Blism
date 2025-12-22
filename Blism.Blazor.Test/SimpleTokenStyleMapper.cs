using Blism.Core;

namespace Blism.Blazor.Test;

public class SimpleTokenStyleMapper : ITokenStyleMapper<SimpleTokenType>
{
	public StyleTokenType MapTokenType(SimpleTokenType tokenType)
	{
		return tokenType switch
		{
			SimpleTokenType.Char => StyleTokenType.String,
			SimpleTokenType.Digit => StyleTokenType.Number,
			SimpleTokenType.Keyword => StyleTokenType.Keyword,
			_ => StyleTokenType.Unknown,
		};
	}
}

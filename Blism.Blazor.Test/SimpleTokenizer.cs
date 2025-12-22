using System.Text.RegularExpressions;
using Blism.Core;

namespace Blism.Blazor.Test;

public partial class SimpleTokenizer : BaseTokenizer<SimpleTokenType>
{
	protected override IEnumerable<(Regex regex, SimpleTokenType type)> GetTokenDefinitions()
	{
		yield return (KeywordRegex(), SimpleTokenType.Keyword);
		yield return (DigitRegex(), SimpleTokenType.Digit);
		yield return (CharRegex(), SimpleTokenType.Char);
	}

	protected override SimpleTokenType UnknownTokenType => SimpleTokenType.Unknown;

	[GeneratedRegex(@"\w")]
	private static partial Regex CharRegex();

	[GeneratedRegex(@"\d")]
	private static partial Regex DigitRegex();

	[GeneratedRegex("keyword")]
	private static partial Regex KeywordRegex();
}

public enum SimpleTokenType
{
	Char,
	Digit,
	Keyword,
	Unknown,
}

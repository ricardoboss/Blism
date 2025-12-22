using System.Text.RegularExpressions;
using Blism.Core;

namespace Blism.Language.Dart.Core;

public class DartTokenizer : BaseTokenizer<DartTokenType>
{
	public static readonly DartTokenizer Instance = new();

	protected override IEnumerable<(Regex regex, DartTokenType type)> GetTokenDefinitions()
	{
		yield return (new(@"\/\/.*"), DartTokenType.Comment);
		yield return (new(@"""[^""]*"""), DartTokenType.String);
		yield return (new("'[^']*'"), DartTokenType.String);
		yield return (
			new(
				@"\b(abstract|as|assert|async|await|break|case|catch|class|const|continue|default|deferred|do|dynamic|else|enum|export|extends|external|factory|false|final|finally|for|get|if|implements|import|in|is|library|new|null|operator|part|rethrow|return|set|static|super|switch|sync|this|throw|true|try|typedef|var|void|while|with|yield)\b"),
			DartTokenType.Keyword);
		yield return (new(@"[\[\]{},:]"), DartTokenType.Punctuation);
		yield return (new(@"[0-9]+(\.[0-9]+)?"), DartTokenType.Number);
		yield return (new("[a-zA-Z_][a-zA-Z0-9_]*"), DartTokenType.Identifier);
		yield return (new(@"\s+"), DartTokenType.Whitespace);
	}

	protected override DartTokenType UnknownTokenType => DartTokenType.Unknown;
}

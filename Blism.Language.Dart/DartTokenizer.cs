using System.Text.RegularExpressions;

namespace Blism.Language.Dart;

public class DartTokenizer : BaseTokenizer<DartTokenType>
{
	protected override IEnumerable<(Regex regex, DartTokenType type)> GetTokenDefinitions()
	{
		yield return (new Regex(@"\/\/.*"), DartTokenType.Comment);
		yield return (new Regex(@"""[^""]*"""), DartTokenType.String);
		yield return (new Regex("'[^']*'"), DartTokenType.String);
		yield return (new Regex(@"\b(abstract|as|assert|async|await|break|case|catch|class|const|continue|default|deferred|do|dynamic|else|enum|export|extends|external|factory|false|final|finally|for|get|if|implements|import|in|is|library|new|null|operator|part|rethrow|return|set|static|super|switch|sync|this|throw|true|try|typedef|var|void|while|with|yield)\b"), DartTokenType.Keyword);
		yield return (new Regex(@"[\[\]{},:]"), DartTokenType.Punctuation);
		yield return (new Regex(@"[0-9]+(\.[0-9]+)?"), DartTokenType.Number);
		yield return (new Regex("[a-zA-Z_][a-zA-Z0-9_]*"), DartTokenType.Identifier);
		yield return (new Regex(@"\s+"), DartTokenType.Whitespace);
	}

	protected override DartTokenType UnknownTokenType => DartTokenType.Unknown;
}

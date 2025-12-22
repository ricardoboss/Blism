using System.Text.RegularExpressions;
using Blism.Core;

namespace Blism.Language.Php.Core;

public class PhpTokenizer : BaseTokenizer<PhpTokenType>
{
	public static readonly PhpTokenizer Instance = new();

	private static readonly string[] Keywords =
	[
		"__halt_compiler()",
		"abstract",
		"and",
		"array()",
		"as",
		"break",
		"callable",
		"case",
		"catch",
		"class",
		"clone",
		"const",
		"continue",
		"declare",
		"default",
		"die()",
		"do",
		"echo",
		"else",
		"elseif",
		"empty()",
		"enddeclare",
		"endfor",
		"endforeach",
		"endif",
		"endswitch",
		"endwhile",
		"eval()",
		"exit()",
		"extends",
		"final",
		"finally",
		"fn",
		"for",
		"foreach",
		"function",
		"global",
		"goto",
		"if",
		"implements",
		"include",
		"include_once",
		"instanceof",
		"insteadof",
		"interface",
		"isset()",
		"list()",
		"match",
		"namespace",
		"new",
		"or",
		"print",
		"private",
		"protected",
		"public",
		"readonly",
		"require",
		"require_once",
		"return",
		"static",
		"switch",
		"throw",
		"trait",
		"try",
		"unset()",
		"use",
		"var",
		"while",
		"xor",
		"yield",
		"yield from",
	];

	protected override IEnumerable<(Regex regex, PhpTokenType type)> GetTokenDefinitions()
	{
		yield return (new(@"\$this"), PhpTokenType.Keyword);
		yield return (new(@"\$\w+"), PhpTokenType.Variable);
		yield return (new(@"""[^""]*"""), PhpTokenType.String);
		yield return (new("'[^']*'"), PhpTokenType.String);
		yield return (new(@"[0-9]+(\.[0-9]+)?"), PhpTokenType.Number);
		yield return (new(@"<\?php"), PhpTokenType.Keyword);
		yield return (new(@"\/\/.*"), PhpTokenType.Comment);

		foreach (var keyword in Keywords)
			yield return (new(@"\b" + Regex.Escape(keyword) + @"\b"), PhpTokenType.Keyword);

		yield return (new("[a-zA-Z_][a-zA-Z0-9_]*"), PhpTokenType.Identifier);
		yield return (new(@"\s+"), PhpTokenType.Whitespace);
	}

	protected override PhpTokenType UnknownTokenType => PhpTokenType.Unknown;
}

using System.Text.RegularExpressions;

namespace Blism.Language.Bash;

public class BashTokenizer : BaseTokenizer<BashTokenType>
{
	public static readonly BashTokenizer Instance = new();

	private static readonly string[] Keywords =
	[
		"if",
		"then",
		"elif",
		"else",
		"fi",
		"for",
		"in",
		"do",
		"done",
		"while",
		"until",
		"case",
		"esac",
		"function",
		"return",
		"break",
		"continue",
		"exit",
		"declare",
		"local",
		"readonly",
		"export",
		"trap",
		"eval",
		"shift",
	];

	private static readonly string[] Commands =
	[
		"echo",
		"read",
		"printf",
		"test",
		"expr",
		"let",
		"cd",
		"pwd",
		"mkdir",
		"rmdir",
		"rm",
		"cp",
		"mv",
		"ls",
		"cat",
		"touch",
		"chmod",
		"chown",
		"ps",
		"kill",
		"sleep",
		"grep",
		"sed",
		"awk",
		"cut",
		"find",
		"xargs",
		"head",
		"tail",
		"sort",
		"uniq",
		"tee",
		"diff",
		"wc",
		"tar",
		"gzip",
		"gunzip",
		"curl",
		"wget",
		"basename",
		"dirname",
		"tr",
		"true",
		"false",
	];

	protected override IEnumerable<(Regex regex, BashTokenType type)> GetTokenDefinitions()
	{
		yield return (new(@"#![^\n]*"), BashTokenType.SheBang);
		yield return (new("#.*"), BashTokenType.Comment);
		yield return (new(@"""[^""]*"""), BashTokenType.String);
		yield return (new(@"'[^']*'"), BashTokenType.String);
		yield return (new(@"\d+(\.\d+)?"), BashTokenType.Number);
		yield return (new("[a-zA-Z0-9_-]+"), BashTokenType.Identifier);
		yield return (new(@"\s+"), BashTokenType.Whitespace);
		yield return (new(@"[\[\]{},:]"), BashTokenType.Punctuation);

		foreach (var keyword in Keywords)
			yield return (new(@"\b" + Regex.Escape(keyword) + @"\b"), BashTokenType.Keyword);

		foreach (var command in Commands)
			yield return (new(@"\b" + Regex.Escape(command) + @"\b"), BashTokenType.Command);
	}

	protected override BashTokenType UnknownTokenType => BashTokenType.Unknown;
}

using System.Text.RegularExpressions;

namespace Blism.Language.Csharp;

public partial class CSharpTokenizer : ITokenizer<CSharpTokenType>
{
	private static readonly string[] Keywords =
	[
		"abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const",
		"continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern",
		"false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface",
		"internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", "params",
		"private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof",
		"stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong",
		"unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while", "add", "allows", "alias",
		"and", "ascending", "args", "async", "await", "by", "descending", "dynamic", "equals", "file", "from", "get",
		"global", "group", "init", "into", "join", "let", "managed", "nameof", "nint", "not", "notnull", "nuint", "on",
		"or", "orderby", "partial", "record", "remove", "required", "scoped", "select", "set", "unmanaged", "value",
		"var", "when", "where", "with", "yield",
	];

	private const string Punctuation = @"[{}()\[\];,.]";

	public IEnumerable<SyntaxToken<CSharpTokenType>> Tokenize(string code)
	{
		var tokenDefinitions = new List<(Regex regex, CSharpTokenType type)>
		{
			(new Regex(@"\/\/.*"), CSharpTokenType.Comment),
			(new Regex(@"\/\*[\s\S]*?\*\/"), CSharpTokenType.Comment),
			(new Regex(@"\s+"), CSharpTokenType.Whitespace),
			(new Regex(@"\d+(\.\d+)?"), CSharpTokenType.Number),
			(new Regex(@"""[^""]*"""), CSharpTokenType.String),
			(new Regex(Punctuation), CSharpTokenType.Punctuation),
			(new Regex(@"\b[_a-zA-Z][_a-zA-Z0-9]*\b"), CSharpTokenType.Identifier),
		};

		foreach (var keyword in Keywords)
		{
			tokenDefinitions.Insert(0,
				(new Regex(@"\b" + Regex.Escape(keyword) + @"\b"), CSharpTokenType.Keyword));
		}

		var index = 0;

		while (index < code.Length)
		{
			var matched = false;

			foreach (var (regex, type) in tokenDefinitions)
			{
				var match = regex.Match(code, index);
				if (!match.Success || match.Index != index)
					continue;

				yield return new()
				{
					Value = match.Value,
					Type = type,
				};

				index += match.Length;
				matched = true;

				break;
			}

			if (matched)
				continue;

			// Default case, handle single characters or unknown tokens
			yield return new()
			{
				Value = code[index].ToString(),
				Type = CSharpTokenType.Unknown,
			};

			index++;
		}
	}
}

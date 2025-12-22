using System.Text.RegularExpressions;
using Blism.Core;

namespace Blism.Language.Yaml.Core;

public class YamlTokenizer : BaseTokenizer<YamlTokenType>
{
	public static readonly YamlTokenizer Instance = new();

	protected override IEnumerable<(Regex regex, YamlTokenType type)> GetTokenDefinitions()
	{
		yield return (new(@"\s+"), YamlTokenType.Whitespace);
		yield return (new(@"\#.*"), YamlTokenType.Comment);
		yield return (new(@"""[^""]*"""), YamlTokenType.Scalar);
		yield return (new("'[^']*'"), YamlTokenType.Scalar);
		yield return (new("[a-zA-Z0-9_-]+:"), YamlTokenType.Key);
		yield return (new(@"[\[\]{},:]"), YamlTokenType.Punctuation);
		yield return (new(@"[a-zA-Z0-9_\-.]+"), YamlTokenType.Value);
	}

	protected override YamlTokenType UnknownTokenType => YamlTokenType.Unknown;

	protected override IEnumerable<SyntaxToken<YamlTokenType>> RefineToken(string value, YamlTokenType type)
	{
		switch (type)
		{
			case YamlTokenType.Key:
				yield return new()
				{
					Value = value.TrimEnd(':'),
					Type = YamlTokenType.Key,
				};

				yield return new()
				{
					Value = ":",
					Type = YamlTokenType.Punctuation,
				};
				break;
			case YamlTokenType.Value:
				if (value.StartsWith("'") || value.StartsWith("\"") || value.All(char.IsDigit) || value == "true" ||
					value == "false")
				{
					yield return new()
					{
						Value = value,
						Type = YamlTokenType.Scalar,
					};

					break;
				}

				goto default;
			default:
				yield return new()
				{
					Value = value,
					Type = type,
				};
				break;
		}
	}
}

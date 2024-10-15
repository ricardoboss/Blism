using System.Text.RegularExpressions;

namespace Blism.Language.Yaml;

public class YamlTokenizer : BaseTokenizer<YamlTokenType>
{
	protected override IEnumerable<(Regex regex, YamlTokenType type)> GetTokenDefinitions()
	{
		yield return (new Regex(@"\s+"), YamlTokenType.Whitespace);
		yield return (new Regex(@"\#.*"), YamlTokenType.Comment);
		yield return (new Regex(@"""[^""]*"""), YamlTokenType.Scalar);
		yield return (new Regex("'[^']*'"), YamlTokenType.Scalar);
		yield return (new Regex("[a-zA-Z0-9_-]+:"), YamlTokenType.Key);
		yield return (new Regex(@"[\[\]{},:]"), YamlTokenType.Punctuation);
		yield return (new Regex(@"[a-zA-Z0-9_\-.]+"), YamlTokenType.Value);
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

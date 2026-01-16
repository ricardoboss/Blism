using Blism.Core;

namespace Blism.Language.Yaml.Core;

public class YamlTokenStyleMapper : ITokenStyleMapper<YamlTokenType>
{
	public static readonly YamlTokenStyleMapper Instance = new();

	public StyleTokenType MapTokenType(YamlTokenType tokenType)
	{
		return tokenType switch
		{
			YamlTokenType.Comment => StyleTokenType.Comment,
			YamlTokenType.Key => StyleTokenType.Identifier,
			YamlTokenType.Value => StyleTokenType.Unknown,
			YamlTokenType.Scalar => StyleTokenType.Scalar,
			YamlTokenType.Punctuation => StyleTokenType.Punctuation,
			YamlTokenType.Whitespace => StyleTokenType.Whitespace,
			_ => StyleTokenType.Unknown,
		};
	}
}

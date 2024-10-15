using System.Text.RegularExpressions;

namespace Blism;

public abstract class BaseTokenizer<TTokenType> : ITokenizer<TTokenType> where TTokenType : Enum
{
	protected abstract IEnumerable<(Regex regex, TTokenType type)> GetTokenDefinitions();

	protected abstract TTokenType UnknownTokenType { get; }

	public IEnumerable<SyntaxToken<TTokenType>> Tokenize(string code)
	{
		var tokenDefinitions = GetTokenDefinitions().ToList();

		var index = 0;

		while (index < code.Length)
		{
			var matched = false;

			foreach (var (regex, type) in tokenDefinitions)
			{
				var match = regex.Match(code, index);
				if (!match.Success || match.Index != index)
					continue;

				foreach (var token in RefineToken(match.Value, type))
					yield return token;

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
				Type = UnknownTokenType,
			};

			index++;
		}
	}

	protected virtual IEnumerable<SyntaxToken<TTokenType>> RefineToken(string value, TTokenType type)
	{
		yield return new()
		{
			Value = value,
			Type = type,
		};
	}
}

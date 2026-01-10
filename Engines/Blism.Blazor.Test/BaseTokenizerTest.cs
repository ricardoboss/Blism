using Blism.Core;

namespace Blism.Blazor.Test;

public static class BaseTokenizerTest
{
	[Theory]
	[TestCaseSource(nameof(TokenizeTestCases))]
	public static void TestTokenize(string code, SyntaxToken<SimpleTokenType>[] expectedTokens)
	{
		var tokenizer = new SimpleTokenizer();

		var tokens = tokenizer.Tokenize(code);

		Assert.That(tokens, Is.EqualTo(expectedTokens).Using(new SyntaxTokenComparer<SimpleTokenType>()));
	}

	private static IEnumerable<object[]> TokenizeTestCases
	{
		get
		{
			yield return
			[
				"1",
				new[] { new SyntaxToken<SimpleTokenType> { Value = "1", Type = SimpleTokenType.Digit } },
			];

			yield return
			[
				"a",
				new[] { new SyntaxToken<SimpleTokenType> { Value = "a", Type = SimpleTokenType.Char } },
			];

			yield return
			[
				"a1",
				new[]
				{
					new SyntaxToken<SimpleTokenType> { Value = "a", Type = SimpleTokenType.Char },
					new SyntaxToken<SimpleTokenType> { Value = "1", Type = SimpleTokenType.Digit },
				},
			];

			yield return
			[
				"keyword",
				new[]
				{
					new SyntaxToken<SimpleTokenType> { Value = "keyword", Type = SimpleTokenType.Keyword },
				},
			];

			yield return
			[
				"keyword1",
				new[]
				{
					new SyntaxToken<SimpleTokenType> { Value = "keyword", Type = SimpleTokenType.Keyword },
					new SyntaxToken<SimpleTokenType> { Value = "1", Type = SimpleTokenType.Digit },
				},
			];

			yield return
			[
				"1keyword",
				new[]
				{
					new SyntaxToken<SimpleTokenType> { Value = "1", Type = SimpleTokenType.Digit },
					new SyntaxToken<SimpleTokenType> { Value = "keyword", Type = SimpleTokenType.Keyword },
				},
			];

			yield return
			[
				"kkeywordd",
				new[]
				{
					new SyntaxToken<SimpleTokenType> { Value = "k", Type = SimpleTokenType.Char },
					new SyntaxToken<SimpleTokenType> { Value = "keyword", Type = SimpleTokenType.Keyword },
					new SyntaxToken<SimpleTokenType> { Value = "d", Type = SimpleTokenType.Char },
				},
			];

			yield return
			[
				"-",
				new[]
				{
					new SyntaxToken<SimpleTokenType> { Value = "-", Type = SimpleTokenType.Unknown },
				},
			];
		}
	}
}

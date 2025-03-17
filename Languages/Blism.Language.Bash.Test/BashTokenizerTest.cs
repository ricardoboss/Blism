using System.Collections.Immutable;

namespace Blism.Language.Bash.Test;

public static class BashTokenizerTest
{
	[Test]
	public static void TestTokenizeSimpleScripts()
	{
		const string source = "dart pub token add http://localhost:5000 --env-var PUBNET_TOKEN";

		var tokenizer = new BashTokenizer();
		var result = tokenizer.Tokenize(source).ToImmutableList();

		Assert.That(result, Has.Count.EqualTo(13));
		Assert.Multiple(() =>
		{
			Assert.That(result[0].Type, Is.EqualTo(BashTokenType.Identifier));
			Assert.That(result[0].Value, Is.EqualTo("dart"));

			Assert.That(result[1].Type, Is.EqualTo(BashTokenType.Whitespace));

			Assert.That(result[2].Type, Is.EqualTo(BashTokenType.Identifier));
			Assert.That(result[2].Value, Is.EqualTo("pub"));

			Assert.That(result[3].Type, Is.EqualTo(BashTokenType.Whitespace));

			Assert.That(result[4].Type, Is.EqualTo(BashTokenType.Identifier));
			Assert.That(result[4].Value, Is.EqualTo("token"));

			Assert.That(result[5].Type, Is.EqualTo(BashTokenType.Whitespace));

			Assert.That(result[6].Type, Is.EqualTo(BashTokenType.Identifier));
			Assert.That(result[6].Value, Is.EqualTo("add"));

			Assert.That(result[7].Type, Is.EqualTo(BashTokenType.Whitespace));

			Assert.That(result[8].Type, Is.EqualTo(BashTokenType.String));
			Assert.That(result[8].Value, Is.EqualTo("http://localhost:5000"));

			Assert.That(result[9].Type, Is.EqualTo(BashTokenType.Whitespace));

			Assert.That(result[10].Type, Is.EqualTo(BashTokenType.Option));
			Assert.That(result[10].Value, Is.EqualTo("--env-var"));

			Assert.That(result[11].Type, Is.EqualTo(BashTokenType.Whitespace));

			Assert.That(result[12].Type, Is.EqualTo(BashTokenType.Identifier));
			Assert.That(result[12].Value, Is.EqualTo("PUBNET_TOKEN"));
		});
	}
}

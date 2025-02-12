namespace Blism.Language.Bash;

public enum BashTokenType
{
	Unknown,
	Punctuation,
	Whitespace,
	Comment,
	String,
	Number,
	Identifier,
	Keyword,
	SheBang,
	Command,
}

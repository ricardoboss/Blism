namespace Blism.Language.Bash.Core;

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
	Option,
	PositionalParameter,
}

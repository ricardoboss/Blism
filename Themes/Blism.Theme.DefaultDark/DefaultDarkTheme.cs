using System.Drawing;
using Blism.Core;

namespace Blism.Theme.DefaultDark;

public class DefaultDarkTheme : ITheme
{
	public static readonly DefaultDarkTheme Instance = new();

	private static readonly TokenStyle Default = new()
	{
		Background = Color.FromArgb(0xff, 0x1e, 0x1e, 0x1e),
		Foreground = Color.FromArgb(0xff, 0xd4, 0xd4, 0xd4),
	};

	public TokenStyle? GetTokenStyle(StyleTokenType tokenType)
	{
		return tokenType switch
		{
			StyleTokenType.Keyword => new()
			{
				Foreground = Color.FromArgb(0xff, 0x47, 0xa2, 0xed),
				TextStyle = TextStyle.Bold,
			},
			StyleTokenType.String => new() { Foreground = Color.FromArgb(0xff, 0xcc, 0x88, 0x4e) },
			StyleTokenType.Number => new() { Foreground = Color.FromArgb(0xff, 0xb3, 0xcc, 0xa4) },
			StyleTokenType.Comment => new()
			{
				Foreground = Color.FromArgb(0xff, 0x69, 0x98, 0x56),
				TextStyle = TextStyle.Italic,
			},
			StyleTokenType.SpecialComment => new()
			{
				Foreground = Color.FromArgb(0xff, 0x69, 0x98, 0x56),
				TextStyle = TextStyle.Italic | TextStyle.Bold,
			},
			StyleTokenType.Type => new() { Foreground = Color.FromArgb(0xff, 0x47, 0xcc, 0xb1) },
			StyleTokenType.Identifier => new() { Foreground = Color.FromArgb(0xff, 0x94, 0xdb, 0xfd) },
			StyleTokenType.SpecialIdentifier => new()
			{
				Foreground = Color.FromArgb(0xff, 0x94, 0xdb, 0xfd),
				TextStyle = TextStyle.Bold,
			},
			StyleTokenType.Scalar => new() { Foreground = Color.FromArgb(0xff, 0xcc, 0x88, 0x4e) },
			StyleTokenType.Unknown => null,
			StyleTokenType.Whitespace => null,
			StyleTokenType.Punctuation => null,
			_ => throw new NotImplementedException($"Style token type {tokenType} is not implemented in this theme"),
		};
	}

	public TokenStyle GetDefaultStyle() => Default;
}

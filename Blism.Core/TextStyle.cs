namespace Blism.Core;

[Flags]
public enum TextStyle
{
	Default = 0,
	Italic = 1 << 0,
	Bold = 1 << 1,
}

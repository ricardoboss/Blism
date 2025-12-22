using System.Drawing;
using System.Text;
using Blism.Core;

namespace Blism.Blazor;

public class TokenStyleCssRenderer : ITokenStyleRenderer
{
	public static readonly TokenStyleCssRenderer Instance = new();

	private readonly StringBuilder sb = new();

	public string Render(TokenStyle? style)
	{
		if (style is null)
			return string.Empty;

		sb.Clear();

		if (style.Background is { } background)
		{
			sb.Append("background:");
			AppendColor(background);
			sb.Append(';');
		}

		if (style.Foreground is { } foreground)
		{
			sb.Append("color:");
			AppendColor(foreground);
			sb.Append(';');
		}

		if ((style.TextStyle & TextStyle.Bold) == TextStyle.Bold)
		{
			sb.Append("font-weight:bold;");
		}

		if ((style.TextStyle & TextStyle.Italic) == TextStyle.Italic)
		{
			sb.Append("font-style:italic;");
		}

		return sb.ToString();
	}

	private void AppendColor(Color color)
	{
		sb.Append('#');
		sb.Append(color.R.ToString("x2"));
		sb.Append(color.G.ToString("x2"));
		sb.Append(color.B.ToString("x2"));

		if (color.A != 255)
			sb.Append(color.A.ToString("x2"));
	}
}

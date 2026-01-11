using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Blism.Core;

namespace Blism.Wpf;

public class SyntaxHighlighter<TTokenType> : UserControl where TTokenType : struct, Enum
{
	private readonly TextBlock _textBlock;
	private readonly ScrollViewer _scrollViewer;

	private readonly Dictionary<TTokenType, (Brush? Fg, Brush? Bg, FontWeight Weight, FontStyle Style)> _styleCache =
		new();

	public SyntaxHighlighter()
	{
		_textBlock = new()
		{
			FontFamily = new("Consolas, Courier New, Monospace"),
			FontSize = 14,
			TextWrapping = TextWrapping.NoWrap,
		};

		_scrollViewer = new()
		{
			HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
			VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
			Content = _textBlock,
		};

		Content = _scrollViewer;
	}

	public static readonly DependencyProperty TokenizerProperty = DependencyProperty.Register(nameof(Tokenizer),
		typeof(ITokenizer<TTokenType>), typeof(SyntaxHighlighter<TTokenType>), new(null, OnRefreshRequired));

	public static readonly DependencyProperty StyleMapperProperty = DependencyProperty.Register(nameof(StyleMapper),
		typeof(ITokenStyleMapper<TTokenType>), typeof(SyntaxHighlighter<TTokenType>), new(null, OnRefreshRequired));

	public static readonly DependencyProperty ThemeProperty = DependencyProperty.Register(nameof(Theme), typeof(ITheme),
		typeof(SyntaxHighlighter<TTokenType>), new(null, OnThemeChanged));

	public static readonly DependencyProperty CodeProperty = DependencyProperty.Register(nameof(Code), typeof(string),
		typeof(SyntaxHighlighter<TTokenType>), new(string.Empty, OnRefreshRequired));

	public ITokenizer<TTokenType>? Tokenizer
	{
		get => (ITokenizer<TTokenType>?)GetValue(TokenizerProperty);
		set => SetValue(TokenizerProperty, value);
	}

	public ITokenStyleMapper<TTokenType>? StyleMapper
	{
		get => (ITokenStyleMapper<TTokenType>?)GetValue(StyleMapperProperty);
		set => SetValue(StyleMapperProperty, value);
	}

	public ITheme? Theme
	{
		get => (ITheme?)GetValue(ThemeProperty);
		set => SetValue(ThemeProperty, value);
	}

	public string? Code
	{
		get => (string?)GetValue(CodeProperty);
		set => SetValue(CodeProperty, value);
	}

	private static void OnThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var control = (SyntaxHighlighter<TTokenType>)d;
		control.ApplyContainerStyle();
		control.RenderTokens();
	}

	private static void OnRefreshRequired(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((SyntaxHighlighter<TTokenType>)d).RenderTokens();
	}

	private void ApplyContainerStyle()
	{
		var defaultStyle = Theme?.GetDefaultStyle();
		var wpfStyle = ConvertToWpfStyle(defaultStyle);

		if (wpfStyle.Fg is { } foreground)
			_textBlock.Foreground = foreground;

		if (wpfStyle.Bg is { } background)
			_scrollViewer.Background = background;

		_textBlock.FontStyle = wpfStyle.Style;
		_textBlock.FontWeight = wpfStyle.Weight;
	}

	private void RenderTokens()
	{
		_textBlock.Inlines.Clear();

		if (string.IsNullOrEmpty(Code))
			return;

		_styleCache.Clear();

		var tokens = Tokenizer?.Tokenize(Code) ?? [];

		foreach (var token in tokens)
		{
			if (!_styleCache.TryGetValue(token.Type, out var wpfStyle))
			{
				var mappedType = StyleMapper?.MapTokenType(token.Type) ?? StyleTokenType.Unknown;
				var tokenStyle = Theme?.GetTokenStyle(mappedType);

				wpfStyle = ConvertToWpfStyle(tokenStyle);
				_styleCache[token.Type] = wpfStyle;
			}

			var run = new Run(token.Value)
			{
				FontWeight = wpfStyle.Weight,
				FontStyle = wpfStyle.Style,
			};

			if (wpfStyle.Fg is { } foreground)
				run.Foreground = foreground;
			else
				run.Foreground = _textBlock.Foreground;

			if (wpfStyle.Bg is { } background)
				run.Background = background;
			else
				run.Background = Brushes.Transparent;

			_textBlock.Inlines.Add(run);
		}
	}

	private static (Brush? Fg, Brush? Bg, FontWeight Weight, FontStyle Style) ConvertToWpfStyle(TokenStyle? style)
	{
		if (style is null)
			return (null, null, FontWeights.Normal, FontStyles.Normal);

		Brush? fg = style.Foreground is { } fgColor ? ToMediaBrush(fgColor) : null;
		Brush? bg = style.Background is { } bgColor ? ToMediaBrush(bgColor) : null;

		var weight = (style.TextStyle & TextStyle.Bold) != 0
			? FontWeights.Bold
			: FontWeights.Normal;

		var fontStyle = (style.TextStyle & TextStyle.Italic) != 0
			? FontStyles.Italic
			: FontStyles.Normal;

		return (fg, bg, weight, fontStyle);
	}

	private static SolidColorBrush ToMediaBrush(System.Drawing.Color drawingColor)
	{
		var mediaColor = Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B);

		var brush = new SolidColorBrush(mediaColor);
		brush.Freeze();
		return brush;
	}
}

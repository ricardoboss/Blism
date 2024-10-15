# Blism

Blism is a simple syntax highlighter library for Blazor.

![Screenshot](https://raw.githubusercontent.com/ricardoboss/Blism/main/.github/assets/screenshot.png)

## Installation

Install the package from NuGet:

```shell
dotnet add package Blism.Language.CSharp # Repeat for other languages
```

## Usage

```html
<SyntaxHighlighter TLanguage="CSharpHighlighterLanguage" TTokenType="CSharpTokenType" Code="@CSharpSource" Language="CSharpHighlighterLanguage.Instance"/>
```

## Supported languages

- C#
- Dart
- YAML

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

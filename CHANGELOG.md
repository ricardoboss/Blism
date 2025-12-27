# Unreleased

- Added `Blism.Bundle.Languages.Blazor` and `Blism.Bundle.Languages` packages

# 2.0.0

- Updated to .NET 10
- BREAKING: Reorganized packages:
  - `Blism.Core` is a pure .NET package, no Blazor dependency
  - `Blism.Blazor` provides Blazor infrastructure
  - `Blism.Language.*` is now `Blism.Language.*.Blazor`
- BREAKING: Themes are now decoupled from Blazor and the language
  - Theme is now a required parameter for `SyntaxHighlighter`
  - Languages must provide a mapping from their tokens to style tokens

# 1.3.3

- Updated to .NET 9
- Added support for tokenizing PHP
- Added dark theme for PHP
- Added `PhpSyntaxHighlighter` component

# 1.3.2

- Noop release

# 1.3.1

- Fix parsing of unquoted URLs in Bash

# 1.3.0

- Added `Style` parameter to all `SyntaxHighlighter` components

# 1.2.4

- Noop release

# 1.2.3

- Noop release

# 1.2.2

- Noop release

# 1.2.1

- Noop release

# 1.2.0

- Added `BashSyntaxHighlighter` component
- Added `CSharpSyntaxHighlighter` component
- Added `DartSyntaxHighlighter` component
- Added `YamlSyntaxHighlighter` component

# 1.1.0

- Added support for tokenizing Bash
- Added dark theme for Bash
- Made highlighter methods virtual so they can be overridden

# 1.0.0

- Initial release
- Added support for tokenizing C#, Dart and YAML
- Added dark themes for C#, Dart and YAML
- Added a `SyntaxHighlighter` blazor component

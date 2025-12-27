# Contributing

## Ways to Contribute

Check out the [`good first issue` issues](https://github.com/ricardoboss/Blism/contribute) to choose an issue to work
on.

## Adding a new language

Adding a language requires a good understanding of the language structure.
You will need to know what a tokenizer is (see [Lexical analysis](https://en.wikipedia.org/wiki/Lexical_analysis))
and how to write one for your target language.

The overall steps to add a language are as follows:

1. Creating a new project
2. Implement the tokenizer
3. Implement the style mapper
4. (optional) Implement the Blazor component

To illustrate how to do this, let's add a fictional language `FooLang` that looks like this:

```
GREETING=Hello
SUBJECT=World
```

### Creating a new project

1. If you haven't already, [fork this repository](https://github.com/ricardoboss/Blism/fork)
2. Create a new C# library in the `Languages/[LANGUAGE NAME]` folder
    - In the solution, create a new solution folder with the name of the language
    - Follow the naming pattern: `Blism.Language.[LANGUAGE NAME].Core`
    - The language names are normalized to [PascalCase](https://stringcase.org/cases/pascal/)
3. Replace the project file contents with the content of a project file from an existing language

For our example language, the project name would be `Blism.Language.FooLang`.
The project would be created at `[repository root]/Languages/Foo/Blism.Language.FooLang.Core/Blism.Language.FooLang.Core.csproj`.

Adhering to this pattern allows adding extension for this language later on (like a test project).

> [!NOTE]
> This naming pattern is required for automatic inclusion in the bundle packages.

### Implement the tokenizer

To implement the tokenizer, you first need to define your languages token types.
These are like the building blocks of any programming language.

The token types must be an enum named `[LANGUAGE NAME]TokenType`.

Our example language might define these types:

```csharp
public enum FooLangTokenType
{
    Unknown,
    Key,
    EqualSign,
    Value,
}
```

The `Unknown` case helps if the tokenizer is unable to determine the correct token type.
It's a good idea to include this case for your language too (you'll see why later).

After defining the types, we can start implementing the tokenizer.
To help make this as painless as possible, Blism provides a class `BaseTokenizer`, which does much of the heavy lifting
for us.

The tokenizer must be a class named `[LANGUAGE NAME]Tokenizer`.

We need to give the `BaseTokenizer` our previously defined token types as a generic parameter.
There are only two abstract signature we need to implement:

- `IEnumerable<(Regex regex, [LANGUAGE NAME]TokenType type)> GetTokenDefinitions()`, and
- `[LANGUAGE NAME]TokenType UnknownTokenType { get; }`

The token definitions basically just map a regular expression to a token type.
This means the `BaseTokenizer` currently doesn't support context-aware keywords, for example.
We are building a context-free tokenizer.

For our simple language, we only need three expressions:
- Key - Match everything up until the first equal sign: `.+(?==)`
- Equal sign - Match an equal sign, but only if it is neither first, nor last character: `(?<=.)=(?=.)`
- Value - Match everything after the first equal sign: `(?<==).+`

These are not the best regular expressions (there are many edge cases), but they'll do the job.

The other thing it wants use to implement is a getter for the "unknown" token type.
This type is used when none of the regexes matched the input.

Your tokenizer should also expose a static, readonly singleton instance (used in the Blazor component).

This is what the tokenizer could look like for our example language:

```csharp
public class FooLangTokenizer : BaseTokenizer<FooLangTokenType>
{
	public static readonly FooLangTokenizer Instance = new();

	protected override IEnumerable<(Regex regex, FooLangTokenType type)> GetTokenDefinitions()
	{
		yield return (new(@".+(?:\=)"), FooLangTokenType.Key);
		yield return (new(@"(?<=.)=(?=.)"), FooLangTokenType.EqualSign);
		yield return (new(@"(?<==).+"), FooLangTokenType.Value);
	}

	protected override FooLangTokenType UnknownTokenType => FooLangTokenType.Unknown; // see?
}
```

Of course, you don't have to use `BaseTokenizer`.
You can totally roll your own tokenizer by implementing `ITokenizer<TTokenType>` and implement context-aware features,
but it would be too complicated to show here.

### Implement the style mapper

Once we have a bunch of tokens, we just need to apply a fresh coat of paint.
To do that, we need to implement a style mapper.

It will map our custom token types to _style_ token types.
These are used in themes to categorize token styles.

The style mapper for our custom language might look like this:

```csharp
public class FooLangTokenStyleMapper : ITokenStyleMapper<FooLangTokenType>
{
	public static readonly FooLangTokenStyleMapper Instance = new();

	public StyleTokenType MapTokenType(FooLangTokenType tokenType)
	{
		return tokenType switch
		{
			FooLangTokenType.Key => StyleTokenType.Identifier,
			FooLangTokenType.EqualSign => StyleTokenType.Punctuation,
			FooLangTokenType.Value => StyleTokenType.String,
			_ => StyleTokenType.Unknown,
		};
	}
}
```

In this case we also provide a singleton `Instance` field for convenience.

The `StyleTokenType` may require new cases for the language you are adding.
This is generally no problem, but the style token types can't get too specific since every theme needs to be able to
handle all possible cases.

### Implement the Blazor component

The last step for adding a new language is adding a Blazor component.

To do that, we add a new project to our language solution folder: `/Languages/[LANGUAGE NAME]/Blism.Language.[LANGUAGE NAME].Blazor/Blism.Language.[LANGUAGE NAME].Blazor.csproj`

Again, replace the contents of the `.csproj`-file with the contents of another languages' `.Blazor.csproj` and adjust
fields like the `<Description>` and the project reference to the language core project.

It will use our tokenizer and our style mapper as a default, so users can simply put a single line of code into their
page without having to wire up everything manually.

The name of the component should be `[LANGUAGE NAME]SyntaxHighlighter`.

For our example language, this might look like this:

`FooLangSyntaxHighlighter.razor`:

```razorhtmldialect
@code {
	[Parameter]
	public required ITokenizer<FooLangTokenType> Tokenizer { get; set; } = FooLangTokenizer.Instance;

	[Parameter]
	public required ITokenStyleMapper<FooLangTokenType> StyleMapper { get; set; } = FooLangTokenStyleMapper.Instance;

	[Parameter, EditorRequired]
	public required ITheme Theme { get; set; }

	[Parameter, EditorRequired]
	public required string Code { get; set; }

	[Parameter]
	public string? Style { get; set; }

}

<SyntaxHighlighter Tokenizer="@Tokenizer" Theme="@Theme" StyleMapper="@StyleMapper" Code="@Code" Style="@Style"/>
```

It conveniently uses the static singleton instances we prepared, so the user doesn't have to pass them into the
component.
Also, we allow custom style to be added using the `Style` parameter.
The only thing the user must supply are the `Code` and `Theme` parameters.

And that's it.
Now you can plop a `<FooLangSyntaxHighlighter Code="HELLO=World" Theme="DefaultDarkTheme.Instance" />` into your Razor
page and you get highlighted code.

## New Releases

Packages are automatically built and released by a GitHub Actions workflow.
To create a new release, just tag a commit with the following pattern: `v[0-9]+.[0-9]+.[0-9]+*`

After building and approval, the built packages are pushed to nuget.org and a release on GitHub is created.
All packages will have the version included in the tag you pushed.

## Code of Conduct

Please read and adhere to our [Code of Conduct](CODE_OF_CONDUCT.md) to ensure a positive and inclusive environment for
all contributors.

### Coding Guidelines

Make sure your code is properly formatted and follows our coding guidelines:

- Format your code using `dotnet format`
- Follow
  the [C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Follow the [.NET Design Guidelines](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/)

## Licensing

When you submit a contribution to this project, you are agreeing to license your code under the same open-source
license as the rest of the project, as specified in the license file (LICENSE.md).

While you retain the copyright to your code, it will be available to the public under the project's license terms.
Ensure that any third-party code or libraries in your contribution are compatible with our project's license.

Note that the project maintainers may update the license in the future, and contributions made after a license change
will be subject to the new terms. Your submission acknowledges and accepts these licensing terms.

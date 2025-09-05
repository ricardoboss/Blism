# Contributing

## Ways to Contribute

Check out the [`good first issue` issues](https://github.com/ricardoboss/Blism/contribute) to choose an issue to work
on.

## Adding a new language

Adding a language requires a good understanding of the language structure.
You will need to know what a tokenizer is (see [Lexical analysis](https://en.wikipedia.org/wiki/Lexical_analysis))
and how to write one for your target language.

The overall steps to add a language are as follows:

1. Create a new project
2. Implement the tokenizer
3. Implement the highlighter
4. (optional) Implement the Blazor component

To illustrate how to do this, let's add a fictional language `FooLang` that looks like this:

```
GREETING=Hello
SUBJECT=World
```

### Creating a new project

1. If you haven't already, [fork this repository](https://github.com/ricardoboss/Blism/fork)
2. Create a new C# library in the `Languages/` folder
    - Follow the naming pattern: `Blism.Language.[LANGUAGE NAME]`
    - The language names are normalized to [PascalCase](https://stringcase.org/cases/pascal/)
3. Replace the project file contents with the content of a project file from an existing language

For our example language, the project name would be `Blism.Language.FooLang`.

### Implement the tokenizer

The implement the tokenizer, you first need to define your languages token types.
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

### Implement the highlighter

Once we have a bunch of tokens, we just need to apply a fresh coat of paint.
To do that, we need to implement a highlighter.

It receives the list of tokens from our tokenizer and, depending on its type, returns styles to be applied to it.

Akin to the other classes, the highlighter must also contain the language name.
The difference is that the highlighter also defines the _theme_ the code will have.
This means the name should also include the _theme name_ it will apply to the tokens.

When adding a language, you should add a "dark" theme.

The highlighter name should therefore be `[LANGUAGE NAME][THEME NAME]Highlighter`.

It must implement the `ITokenTypeHighlighter` interface.

For our example language, this might look like this:

```csharp
public class FooLangDarkHighlighter : ITokenTypeHighlighter<FooLangTokenType>
{
    public static readonly FooLangDarkHighlighter Instance = new();

    public virtual string GetCss(FooLangTokenType tokenType)
    {
        return tokenType switch
        {
            FooLangTokenType.Key => "color: #94dbfd;",
            FooLangTokenType.Value => "color: #cc884e;",
            _ => "",
        };
    }

    public virtual string GetDefaultCss()
    {
        return "color: #d4d4d4; background-color: #1e1e1e;";
    }
}
```

Notice three things here:

- We also provide a singleton instance of the highlighter for use in the Blazor component later
- The methods are `virtual`, meaning a user who doesn't like a single color can override this method and handle a single
  case differently
- We are intentionally not handling the `FooLangTokenType.EqualsSign` case. It will be colored according to our default
  CSS

This concludes implementing the highlighter!
If you want to, you can add more themes in the same way.

### Implement the Blazor component

The last step for adding a new language is adding a Blazor component.

It will use our tokenizer and the dark theme as a default, so users can simply put a single line of code into their page
without having to wire up everything manually.

The name of the component should be `[LANGUAGE NAME]SyntaxHighlighter`.

For our example language, this might look like this:

`FooLangSyntaxHighlighter.razor`:

```razorhtmldialect
@code {
	[Parameter]
	public required ITokenizer<FooLangTokenType> Tokenizer { get; set; } = FooLangTokenizer.Instance;

	[Parameter]
	public required ITokenTypeHighlighter<FooLangTokenType> Highlighter { get; set; } = FooLangDarkHighlighter.Instance;

	[Parameter, EditorRequired]
	public required string Code { get; set; }

	[Parameter]
	public string? Style { get; set; }

}

<SyntaxHighlighter Tokenizer="@Tokenizer" Highlighter="@Highlighter" Code="@Code" Style="@Style"/>
```

It conveniently uses the static singleton instance we prepared, so the user doesn't have to pass them into the
component.
Also, we allow custom style to be added using the `Style` parameter.
The only thing the user must supply is the `Code` parameter.

And that's it.
Now you can plop a `<FooLangSyntaxHighlighter Code="HELLO=World" />` into your Razor page and you get highlighted code.

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

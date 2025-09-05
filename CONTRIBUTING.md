# Contributing

## Ways to Contribute

Check out the [`good first issue` issues](https://github.com/ricardoboss/Blism/contribute) to choose an issue to work
on.

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

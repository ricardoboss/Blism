using Bunit;

namespace Blism.Blazor.Test;

public abstract class BunitTestContext : TestContextWrapper
{
	[SetUp]
	public void Setup() => TestContext = new();

	[TearDown]
	public void TearDown() => TestContext?.Dispose();
}

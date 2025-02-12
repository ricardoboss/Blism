using Bunit;

namespace Blism.Test;

public abstract class BunitTestContext : TestContextWrapper
{
	[SetUp]
	public void Setup() => TestContext = new();

	[TearDown]
	public void TearDown() => TestContext?.Dispose();
}

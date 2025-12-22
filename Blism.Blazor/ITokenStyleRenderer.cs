using Blism.Core;

namespace Blism.Blazor;

public interface ITokenStyleRenderer
{
	string Render(TokenStyle? style);
}

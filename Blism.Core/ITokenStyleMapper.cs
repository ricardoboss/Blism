namespace Blism.Core;

public interface ITokenStyleMapper<in TTokenType> where TTokenType : Enum
{
	StyleTokenType MapTokenType(TTokenType tokenType);
}

using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IMoveDataHandler
    {
        T GetAttributeAs<T>(IMove move, string attributeName) where T : IModel;
    }
}

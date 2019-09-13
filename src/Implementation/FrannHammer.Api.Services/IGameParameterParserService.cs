using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services
{
    public interface IGameParameterParserService
    {
        Games ParseGame();
    }
}
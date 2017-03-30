using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IDataCreationService
    {
        T Add<T>(T model) where T : IModel;
    }
}

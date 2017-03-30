using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IDataUpdaterService
    {
        T Update<T>(T model) where T : IModel;
    }
}

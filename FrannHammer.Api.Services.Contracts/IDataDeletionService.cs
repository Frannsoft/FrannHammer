using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IDataDeletionService
    {
        void Delete<T>(int id) where T : IModel;
    }
}

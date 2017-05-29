using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Tests.ApiServiceFactories
{
    public abstract class ApiServiceFactory<T>
        where T : IModel
    {
        public abstract ICrudService<T> CreateService(IRepository<T> repository);
    }
}
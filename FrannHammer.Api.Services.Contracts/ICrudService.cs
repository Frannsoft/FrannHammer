using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface ICrudService<T> : IWriterService<T>, IReaderService<T>
        where T : IModel
    { }
}
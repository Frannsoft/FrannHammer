using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IMoveService : IWriterService<IMove>, IReaderService<IMove>
    {
    }
}

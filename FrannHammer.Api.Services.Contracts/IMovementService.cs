using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IMovementService : IWriterService<IMovement>, IReaderService<IMovement>
    {
    }
}

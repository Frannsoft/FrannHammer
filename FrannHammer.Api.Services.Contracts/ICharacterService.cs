using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface ICharacterService<T> : IWriterService<T>, IReaderService<T>
        where T : IModel
    {
    }

    public interface ICharacterService : IWriterService<ICharacter>, IReaderService<ICharacter>
    {
    }
}

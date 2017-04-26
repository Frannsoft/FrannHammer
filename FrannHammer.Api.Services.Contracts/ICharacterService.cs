using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface ICharacterService : IWriterService<ICharacter>, IReaderService<ICharacter>
    {
    }
}

﻿using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface ICharacterAttributeRowService : IWriterService<ICharacterAttributeRow>, IReaderService<ICharacterAttributeRow>
    {
    }
}

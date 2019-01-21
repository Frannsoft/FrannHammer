using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.Api.Services
{
    public class DefaultCharacterAttributeService : OwnerBasedApiService<ICharacterAttributeRow>, ICharacterAttributeRowService
    {
        private readonly ICharacterAttributeNameProvider _characterAttributeNameProvider;

        public DefaultCharacterAttributeService(IRepository<ICharacterAttributeRow> characterAttributeRowRepository,
            ICharacterAttributeNameProvider characterAttributeNameProvider, string game)
            : base(characterAttributeRowRepository, game)
        {
            Guard.VerifyObjectNotNull(characterAttributeNameProvider, nameof(characterAttributeNameProvider));
            _characterAttributeNameProvider = characterAttributeNameProvider;
        }

        public override IEnumerable<ICharacterAttributeRow> GetAllWhereCharacterNameIs(string name)
        {
            Guard.VerifyStringIsNotNullOrEmpty(name, nameof(name));
            return GetAllWhere(item => item.Owner.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<ICharacterAttributeName> GetAllTypes()
        {
            return GetAll().Select(attr => attr.Name).Distinct().Select(name => _characterAttributeNameProvider.Create(name));
        }

        public IEnumerable<ICharacterAttributeRow> GetSingleWithNameAndMatchingCharacterOwnerId(string name, int id)
        {
            return GetAllWhere(attrRow =>
                attrRow.OwnerId == id && attrRow.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<ICharacterAttributeRow> GetAllWithNameAndMatchingCharacterOwner(string attributeName, string name)
        {
            return GetAllWhere(attrRow =>
                attrRow.Owner.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                attrRow.Name.Equals(attributeName, StringComparison.OrdinalIgnoreCase));
        }
    }
}

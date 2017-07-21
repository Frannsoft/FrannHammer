using System;
using System.Collections.Generic;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using System.Linq;

namespace FrannHammer.Api.Services
{
    public class DefaultCharacterAttributeService : OwnerBasedApiService<ICharacterAttributeRow>, ICharacterAttributeRowService
    {
        private readonly ICharacterAttributeNameProvider _characterAttributeNameProvider;

        public DefaultCharacterAttributeService(IRepository<ICharacterAttributeRow> characterAttributeRowRepository,
            ICharacterAttributeNameProvider characterAttributeNameProvider)
            : base(characterAttributeRowRepository)
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
    }
}

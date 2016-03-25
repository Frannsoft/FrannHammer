using System.Linq;
using Kurogane.Data.RestApi.Models;
using System.Collections.Generic;
using KuroganeHammer.Data.Core;

namespace Kurogane.Data.RestApi.Infrastructure
{
    public interface ICharacterAttributeRepository : IRepository<CharacterAttribute>
    {
        CharacterAttribute GetAttribute(int id);
        List<CharacterAttribute> GetCharacterAttributesByName(string name);
        List<CharacterAttribute> GetCharacterAttributesByCharacter(int ownerId);
        List<CharacterAttribute> GetCharacterAttributesByAttributeType(CharacterAttributes attributeType);
    }

    public class CharacterAttributeRepository : RepositoryBase<CharacterAttribute>, ICharacterAttributeRepository
    {
        public CharacterAttributeRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public CharacterAttribute GetAttribute(int id)
        {
            var attribute = DbContext.CharacterAttributes
                .FirstOrDefault(a => a.Id == id);

            return attribute;
        }

        public List<CharacterAttribute> GetCharacterAttributesByName(string name)
        {
            List<CharacterAttribute> attributes;

            using (var context = new Sm4ShEntities())
            {
                attributes = context.CharacterAttributes.Where(a => a.Name.Equals(name))
                    .ToList();
            }

            return attributes;
        }

        public List<CharacterAttribute> GetCharacterAttributesByCharacter(int ownerId)
        {
            List<CharacterAttribute> attributes;

            using (var context = new Sm4ShEntities())
            {
                attributes = context.CharacterAttributes.Where(a => a.OwnerId == ownerId)
                    .ToList();
            }

            return attributes;
        }

        public List<CharacterAttribute> GetCharacterAttributesByAttributeType(CharacterAttributes attributeType)
        {
            List<CharacterAttribute> attributes;

            var attributeAsString = attributeType.ToString();  //EF only support primitive comparisons at this level
            using (var context = new Sm4ShEntities())
            {
                attributes = context.CharacterAttributes.Where(a => a.AttributeType.ToString().Equals(attributeAsString))
                    .ToList();
            }

            return attributes;
        }
    }
}
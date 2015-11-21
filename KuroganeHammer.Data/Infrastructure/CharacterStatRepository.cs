using KuroganeHammer.Model;
using System.Collections.Generic;
using System.Linq;

namespace KuroganeHammer.Data.Infrastructure
{
    public interface ICharacterStatRepository : IRepository<CharacterStat>
    {
        CharacterStat GetCharacter(int id);
    }

    public class CharacterStatRepository : RepositoryBase<CharacterStat>, ICharacterStatRepository
    {
        public CharacterStatRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public CharacterStat GetCharacter(int id)
        {
            var character = this.DbContext.Characters.Where(c => c.OwnerId == id)
                .FirstOrDefault();
            return character;
        }
    }
}

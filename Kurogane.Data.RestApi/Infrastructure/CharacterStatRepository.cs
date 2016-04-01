using Kurogane.Data.RestApi.Models;
using System.Linq;

namespace Kurogane.Data.RestApi.Infrastructure
{
    public interface ICharacterStatRepository : IRepository<CharacterStat>
    {
        //CharacterStat GetCharacter(int id);
    }

    public class CharacterStatRepository : RepositoryBase<CharacterStat>, ICharacterStatRepository
    {
        public CharacterStatRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        //public CharacterStat GetCharacter(int id)
        //{
        //    var character = DbContext.Characters
        //        .FirstOrDefault(c => c.OwnerId == id);
        //    return character;
        //}

        //public CharacterStat GetCharacter(int id)
        //{
        //    var character = DbContext.Characters.Find(id);
        //}
    }
}

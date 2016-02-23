using Kurogane.Data.RestApi.Models;
using Kurogane.Data.RestApi.Providers;

namespace Kurogane.Data.RestApi.DTOs
{
    public class BaseDTO
    {
        public string CharacterName { get; set; }
        public int Id { get; set; }
        public string ThumbnailUrl { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }

        public BaseDTO(Stat stat, ICharacterStatService characterStatService)
        {
            this.Id = stat.Id;
            this.OwnerId = stat.OwnerId;
            this.Name = stat.Name;

           CharacterStat character = characterStatService.GetCharacter(stat.OwnerId);
           this.CharacterName = character.Name;
           this.ThumbnailUrl = character.ThumbnailUrl;
        }

        public BaseDTO()
        { }

        public override bool Equals(object obj)
        {
            bool retVal = false;
            BaseDTO baseComp = obj as BaseDTO;

            if(baseComp != null)
            {
                if(this.CharacterName.Equals(baseComp.CharacterName) &&
                    this.Name.Equals(baseComp.Name) &&
                    this.OwnerId.Equals(baseComp.OwnerId) &&
                    this.ThumbnailUrl.Equals(baseComp.ThumbnailUrl))
                {
                    retVal = true;
                }
            }

            return retVal;
        }

        public override int GetHashCode()
        {
            return new
            {
                CharacterName,
                ThumbnailUrl,
                OwnerId,
                Name
            }.GetHashCode();
        }
    }
}

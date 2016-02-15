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
    }
}

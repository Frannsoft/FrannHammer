using System.Diagnostics.CodeAnalysis;
using Kurogane.Data.RestApi.Models;
using Kurogane.Data.RestApi.Services;

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
            Id = stat.Id;
            OwnerId = stat.OwnerId;
            Name = stat.Name;

            var character = characterStatService.GetCharacter(stat.OwnerId);
            CharacterName = character.Name;
            ThumbnailUrl = character.ThumbnailUrl;
        }

        public BaseDTO()
        { }

        public override bool Equals(object obj)
        {
            var retVal = false;
            var baseComp = obj as BaseDTO;

            if (baseComp != null)
            {
                if (CharacterName.Equals(baseComp.CharacterName) &&
                    Name.Equals(baseComp.Name) &&
                    OwnerId.Equals(baseComp.OwnerId) &&
                    ThumbnailUrl.Equals(baseComp.ThumbnailUrl))
                {
                    retVal = true;
                }
            }

            return retVal;
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
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

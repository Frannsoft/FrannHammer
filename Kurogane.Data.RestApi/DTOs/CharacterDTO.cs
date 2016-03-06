using System.Diagnostics.CodeAnalysis;
using Kurogane.Data.RestApi.Models;

namespace Kurogane.Data.RestApi.DTOs
{
    public class CharacterDTO
    {
        public int Id { get; set; }
        public string ThumbnailUrl { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
        public string MainImageUrl { get; set; }
        public string Description { get; set; }

        public CharacterDTO(CharacterStat stat)
        {
            Style = stat.Style;
            Description = stat.Description;
            MainImageUrl = stat.MainImageUrl;
            Id = stat.Id;
            ThumbnailUrl = stat.ThumbnailUrl;
            OwnerId = stat.OwnerId;
            Name = stat.Name;
        }

        public CharacterDTO()
        { }

        public override bool Equals(object obj)
        {
            var retVal = false;

            var compDto = obj as CharacterDTO;

            if(compDto != null)
            {
                if(Description.Equals(compDto.Description) &&
                    MainImageUrl.Equals(compDto.MainImageUrl) &&
                    Name.Equals(compDto.Name) &&
                    OwnerId.Equals(compDto.OwnerId) &&
                    Style.Equals(compDto.Style) &&
                    ThumbnailUrl.Equals(compDto.ThumbnailUrl))
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
                Description,
                MainImageUrl,
                Name,
                OwnerId,
                Style,
                ThumbnailUrl
            }.GetHashCode();
        }

    }
}

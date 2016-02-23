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
            bool retVal = false;

            CharacterDTO compDTO = obj as CharacterDTO;

            if(compDTO != null)
            {
                if(this.Description.Equals(compDTO.Description) &&
                    this.MainImageUrl.Equals(compDTO.MainImageUrl) &&
                    this.Name.Equals(compDTO.Name) &&
                    this.OwnerId.Equals(compDTO.OwnerId) &&
                    this.Style.Equals(compDTO.Style) &&
                    this.ThumbnailUrl.Equals(compDTO.ThumbnailUrl))
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

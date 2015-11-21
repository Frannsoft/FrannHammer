using KuroganeHammer.Model;
using KuroganeHammer.Service;

namespace Kurogane.Data.RestApi.DTOs
{
    public class CharacterDTO : BaseDTO
    {
        public string Style { get; set; }
        public string MainImageUrl { get; set; }
        public string Description { get; set; }

        public CharacterDTO(CharacterStat stat, ICharacterStatService characterStatService)
            : base(stat, characterStatService)
        {
            Style = stat.Style;
            Description = stat.Description;
            MainImageUrl = stat.MainImageUrl;
        }
    }
}

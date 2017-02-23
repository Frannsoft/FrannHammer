namespace FrannHammer.Models.DTOs
{
    public class AutocancelDto : BaseMetaDto, IMoveIdEntity
    {
        public string Cancel1 { get; set; }
        public string Cancel2 { get; set; }
    }
}
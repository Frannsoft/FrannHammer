namespace FrannHammer.Models
{
    public abstract class BaseCharacterAttribute : BaseModel
    {
        public Character Character { get; set; }
        public int CharacterId { get; set; }
    }
}
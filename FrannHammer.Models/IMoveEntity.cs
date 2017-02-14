namespace FrannHammer.Models
{
    public interface IMoveEntity : IEntity
    {
        int OwnerId { get; set; }    
    }
}

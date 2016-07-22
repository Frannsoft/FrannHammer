using System;

namespace FrannHammer.Models
{
    public interface IMoveEntity : IEntity
    {
        int OwnerId { get; set; }    
    }

    public interface IMoveIdEntity : IEntity
    {
        int MoveId { get; set; }
    }

    public interface IEntity
    {
        int Id { get; set; }
        DateTime LastModified { get; set; }
    }
}

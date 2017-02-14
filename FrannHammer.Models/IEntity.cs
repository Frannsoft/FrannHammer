using System;

namespace FrannHammer.Models
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime LastModified { get; set; }
    }
}
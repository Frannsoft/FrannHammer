using System;

namespace FrannHammer.Domain.Contracts
{
    public interface IModel
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}

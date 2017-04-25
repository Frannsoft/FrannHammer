using MongoDB.Bson;

namespace FrannHammer.Domain.Contracts
{
    public interface IModel
    {
        string Id { get; set; }
        string Name { get; set; }
    }
}

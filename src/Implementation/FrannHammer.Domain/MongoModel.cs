using FrannHammer.Domain.Contracts;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FrannHammer.Domain
{
    public abstract class MongoModel : IModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string InstanceId { get; set; }

        [FriendlyName(FriendlyNameCommonConstants.Name)]
        public string Name { get; set; }
    }
}
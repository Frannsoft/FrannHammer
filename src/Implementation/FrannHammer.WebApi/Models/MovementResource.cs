using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebApi.Models
{
    public class MovementResource : Resource, IMovement
    {
        public string InstanceId { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public string Owner { get; set; }
        public string Value { get; set; }
    }
}
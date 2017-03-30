using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    public class Movement : IMovement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public string Value { get; set; }
    }
}

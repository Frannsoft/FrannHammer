using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class WebMovement : IMovement
    {
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public string Value { get; set; }
        public int Id { get; set; }
    }
}

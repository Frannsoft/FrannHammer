namespace FrannHammer.WebScraping.Domain.Contracts
{
    public interface IAttributeValue
    {
        string Name { get; set; }
        string Value { get; set; }
        string AttributeFlag { get; set; }
    }
}
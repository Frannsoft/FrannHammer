using FrannHammer.Utility;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class DefaultAttributeValue : IAttributeValue
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string AttributeFlag { get; set; }

        public DefaultAttributeValue()
        { }

        public DefaultAttributeValue(string name, string value, string flag)
        {
            Guard.VerifyStringIsNotNullOrEmpty(name, nameof(name));
            Guard.VerifyStringIsNotNullOrEmpty(value, nameof(value));
            Guard.VerifyStringIsNotNullOrEmpty(flag, nameof(flag));

            Name = name;
            Value = value;
            AttributeFlag = flag;
        }
    }
}

using System;

namespace FrannHammer.WebScraping
{
    public class InstanceIdGenerator : IInstanceIdGenerator
    {
        public string GenerateId() => Guid.NewGuid().ToString().Replace("-", string.Empty);
    }
}

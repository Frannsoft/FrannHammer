using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    public abstract class BaseModel : IModel
    {
        public string InstanceId { get; set; }

        [FriendlyName(FriendlyNameCommonConstants.Name)]
        public string Name { get; set; }

        [FriendlyName("game")]
        public Games Game { get; set; }
    }
}
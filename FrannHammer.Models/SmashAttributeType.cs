using System;

namespace FrannHammer.Models
{
    public class SmashAttributeType : BaseSmashAttributeTypeModel, IEntity
    {
        public DateTime LastModified { get; set; }
    }
}
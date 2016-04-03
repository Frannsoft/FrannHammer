using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuroganeHammer.Data.Api.DTOs
{
    public class CharacterAttributeDto
    {
        public int OwnerId { get; set; }
        public string Rank { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public SmashAttributeTypeDto SmashAttributeTypeDto { get; set; }
        public int SmashAttributeTypeId { get; set; }
    }

    
}
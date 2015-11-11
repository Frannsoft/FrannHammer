using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuroganeHammer.Data.Core.Model.Stats
{
    [Table("MovementStats")]
    public class MovementStat : Stat
    {
        public string Value { get; set; }

        public MovementStat(string name, int ownerId, string value)
            : base(name, ownerId)
        {
            Value = value;
        }

        public MovementStat()
        { }
    }
}

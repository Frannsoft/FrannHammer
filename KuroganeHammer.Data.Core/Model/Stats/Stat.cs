using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace KuroganeHammer.Data.Core.Model.Stats
{
    [Serializable]
    public class Stat
    {
        [Required]
        public string Name { get; private set; }

        [Required]
        public int OwnerId { get; private set; }

        [Required]
        public string RawName { get; private set; }

        [Key]
        public int Id { get; set; }

        public Stat(string name, int ownerId, string rawName)
        {
            Name = name;
            OwnerId = ownerId;
            RawName = rawName.Trim();
        }

        public Stat()
        { }
    }
}

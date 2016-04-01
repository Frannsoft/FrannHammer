using System;

namespace Kurogane.Data.RestApi.Models
{
    public class Stat
    {
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public int Id { get; set; }
        public DateTime LastModified { get; set; }

        public Stat(string name, int ownerId)
        {
            Name = name;
            OwnerId = ownerId;
        }

        public Stat()
        { }
    }
}

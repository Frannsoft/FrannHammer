using System;

namespace KuroganeHammer.Data.Api.Models
{
    public class Character
    {
        public string Style { get; set; }
        public string MainImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Description { get; set; }
        public string ColorTheme { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime LastModified { get; set; }
    }
}
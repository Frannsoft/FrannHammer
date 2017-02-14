using System;

namespace FrannHammer.Models.DTOs
{
    public class BaseMetaDto
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int MoveId { get; set; }
        public string MoveName { get; set; }
        public string Notes { get; set; }
        public string RawValue { get; set; }
        public DateTime LastModified { get; set; }
    }
}
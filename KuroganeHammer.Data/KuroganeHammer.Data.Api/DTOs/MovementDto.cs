using System;
using KuroganeHammer.Data.Api.Models;

namespace KuroganeHammer.Data.Api.DTOs
{
    public class MovementDto
    {
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public int Id { get; set; }
        public DateTime LastModified { get; set; }
        public string Value { get; set; }
        public string CharacterName { get; set; }
        public string ThumbnailUrl { get; set; }

        public MovementDto(string name, int ownerId, int id, DateTime lastModified, string value, Character character)
        {
            Name = name;
            OwnerId = ownerId;
            Id = id;
            LastModified = lastModified;
            Value = value;
            CharacterName = character.Name;
            ThumbnailUrl = character.ThumbnailUrl;
        }

        public MovementDto(Movement movement, Character character)
        {
            Name = movement.Name;
            OwnerId = movement.OwnerId;
            Id = movement.Id;
            LastModified = movement.LastModified;
            Value = movement.Value;
            CharacterName = character.Name;
            ThumbnailUrl = character.ThumbnailUrl;
        }

        public MovementDto()
        { }
    }
}
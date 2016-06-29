using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.Models.DTOs
{
    /// <summary>
    /// Contains the concept of a 'row' of <see cref="CharacterAttribute"/> data.
    /// 
    /// This is because each <see cref="CharacterAttribute"/> consists of a single attribute value
    /// rather than a row of attribute value that KH currently creates his attribute pages from.
    /// 
    /// The way <see cref="CharacterAttribute"/>'s are stored in the DB means if the individual values brought back need
    ///  to change in the future it's fairly straightforward to do so on the back and the frontend won't 
    /// need to change as much (if at all).
    /// 
    /// </summary>
    public class CharacterAttributeRowDto
    {
        public string Rank { get; set; }
        public int SmashAttributeType { get; set; }
        public int OwnerId { get; set; }
        public string CharacterName { get; set; }
        public string ThumbnailUrl { get; set; }
        public List<string> RawHeaders { get; set; }
        public List<CharacterAttributeDto> RawValues { get; set; }
        public List<CharacterAttributeKeyValuePair> ParsedValues { get; set; }

        public CharacterAttributeRowDto(string rank, int attributeType, int ownerId, List<CharacterAttributeKeyValuePair> values, string characterName, string thumbnailUrl)
        {
            Rank = rank;
            SmashAttributeType = attributeType;
            OwnerId = ownerId;
            ParsedValues = values;
            RawHeaders = values.Select(v => v.KeyName).ToList();
            RawValues = values.Select(v => v.ValueCharacterAttributeDto).ToList();
            CharacterName = characterName;
            ThumbnailUrl = thumbnailUrl;
        }

        public CharacterAttributeRowDto()
        { }
    }

    public class CharacterAttributeKeyValuePair
    {
        public string KeyName { get; set; }
        public CharacterAttributeDto ValueCharacterAttributeDto { get; set; }
    }
}
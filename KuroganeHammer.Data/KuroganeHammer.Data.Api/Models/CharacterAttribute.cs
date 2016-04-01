namespace KuroganeHammer.Data.Api.Models
{
    public class CharacterAttribute
    {
        public int OwnerId { get; set; }
        public string Rank { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }

        public SmashAttributeType SmashAttributeType { get; set; }

        //public CharacterAttribute(string rank, string characterName,
        //    string name, string value, SmashAttributeType smashAttributeType)
        //{
        //    Rank = rank;
        //    OwnerId = GetOwnerIdFromCharacterName(characterName);
        //    Name = name;
        //    Value = value;
        //    SmashAttributeType = smashAttributeType;
        //}

        //public CharacterAttribute()
        //{ }

        //private int GetOwnerIdFromCharacterName(string name)
        //{
        //    var characterId = (Characters)Enum.Parse(typeof(Characters), name, true);
        //    return (int)characterId;
        //}
    }
}
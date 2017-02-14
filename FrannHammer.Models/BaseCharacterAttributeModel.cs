namespace FrannHammer.Models
{
    public class BaseCharacterAttributeModel
    {
        protected bool Equals(BaseCharacterAttributeModel other)
        {
            return OwnerId == other.OwnerId && string.Equals(Rank, other.Rank)
                   && string.Equals(Value, other.Value) && string.Equals(Name, other.Name)
                   && Id == other.Id && SmashAttributeTypeId == other.SmashAttributeTypeId
                   && CharacterAttributeTypeId == other.CharacterAttributeTypeId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as BaseCharacterAttributeModel;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = OwnerId;
                hashCode = (hashCode * 397) ^ (Rank?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Value?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Name?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ Id;
                hashCode = (hashCode * 397) ^ SmashAttributeTypeId;
                hashCode = (hashCode * 397) ^ CharacterAttributeTypeId.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(BaseCharacterAttributeModel left, BaseCharacterAttributeModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BaseCharacterAttributeModel left, BaseCharacterAttributeModel right)
        {
            return !Equals(left, right);
        }

        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Rank { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public int SmashAttributeTypeId { get; set; }
        public int? CharacterAttributeTypeId { get; set; }
    }
}
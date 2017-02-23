namespace FrannHammer.Models
{
    public class BaseCharacterAttributeTypeModel
    {
        protected bool Equals(BaseCharacterAttributeTypeModel other)
        {
            return Id == other.Id && string.Equals(Name, other.Name) && NotationId == other.NotationId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as BaseCharacterAttributeTypeModel;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode*397) ^ (Name?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ NotationId;
                return hashCode;
            }
        }

        public static bool operator ==(BaseCharacterAttributeTypeModel left, BaseCharacterAttributeTypeModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BaseCharacterAttributeTypeModel left, BaseCharacterAttributeTypeModel right)
        {
            return !Equals(left, right);
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int NotationId { get; set; }
    }
}
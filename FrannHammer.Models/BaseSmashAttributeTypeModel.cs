namespace FrannHammer.Models
{
    public class BaseSmashAttributeTypeModel
    {
        protected bool Equals(BaseSmashAttributeTypeModel other)
        {
            return Id == other.Id && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as BaseSmashAttributeTypeModel;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id*397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        public static bool operator ==(BaseSmashAttributeTypeModel left, BaseSmashAttributeTypeModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BaseSmashAttributeTypeModel left, BaseSmashAttributeTypeModel right)
        {
            return !Equals(left, right);
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
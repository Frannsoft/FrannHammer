using System;

namespace FrannHammer.Models
{
    public abstract class ThrowTypeBaseModel
    {
        protected bool Equals(ThrowTypeBaseModel other)
        {
            return Id == other.Id && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as ThrowTypeBaseModel;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id*397) ^ (Name?.GetHashCode() ?? 0);
            }
        }

        public static bool operator ==(ThrowTypeBaseModel left, ThrowTypeBaseModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ThrowTypeBaseModel left, ThrowTypeBaseModel right)
        {
            return !Equals(left, right);
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ThrowTypeDto : ThrowTypeBaseModel
    { } 

    public class ThrowType : ThrowTypeBaseModel
    {
        public DateTime LastModified { get; set; }
    }

}

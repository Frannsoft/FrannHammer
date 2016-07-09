using System;

namespace FrannHammer.Models
{
    public abstract class ThrowBaseModel
    {
        protected bool Equals(ThrowBaseModel other)
        {
            return Id == other.Id && MoveId == other.MoveId && ThrowTypeId == other.ThrowTypeId && WeightDependent == other.WeightDependent;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as ThrowBaseModel;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode*397) ^ MoveId;
                hashCode = (hashCode*397) ^ ThrowTypeId;
                hashCode = (hashCode*397) ^ WeightDependent.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(ThrowBaseModel left, ThrowBaseModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ThrowBaseModel left, ThrowBaseModel right)
        {
            return !Equals(left, right);
        }

        public int Id { get; set; }
        public int MoveId { get; set; }
        public int ThrowTypeId { get; set; }
        public bool WeightDependent { get; set; }
    }

    public class ThrowDto : ThrowBaseModel
    { }

    public class Throw : ThrowBaseModel
    {
        public Move Move { get; set; }
        public ThrowType ThrowType { get; set; }
        public DateTime LastModified { get; set; }
    }
}

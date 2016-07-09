namespace FrannHammer.Models.DTOs
{
    public abstract class BaseMoveHitboxMetaDto
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int MoveId { get; set; }
        public string MoveName { get; set; }
        public string Notes { get; set; }
        public string RawValue { get; set; }

        public string Hitbox1 { get; set; }
        public string Hitbox2 { get; set; }
        public string Hitbox3 { get; set; }
        public string Hitbox4 { get; set; }
        public string Hitbox5 { get; set; }
        public string Hitbox6 { get; set; }

        protected bool Equals(BaseMoveHitboxMetaDto other)
        {
            return Id == other.Id && OwnerId == other.OwnerId && MoveId == other.MoveId 
                && string.Equals(Notes, other.Notes) && string.Equals(RawValue, other.RawValue) 
                && string.Equals(Hitbox1, other.Hitbox1) && string.Equals(Hitbox2, other.Hitbox2) 
                && string.Equals(Hitbox3, other.Hitbox3) && string.Equals(Hitbox4, other.Hitbox4) 
                && string.Equals(Hitbox5, other.Hitbox5) && string.Equals(Hitbox6, other.Hitbox6);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as BaseMoveHitboxMetaDto;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode*397) ^ OwnerId;
                hashCode = (hashCode*397) ^ MoveId;
                hashCode = (hashCode*397) ^ (Notes != null ? Notes.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (RawValue != null ? RawValue.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Hitbox1 != null ? Hitbox1.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Hitbox2 != null ? Hitbox2.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Hitbox3 != null ? Hitbox3.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Hitbox4 != null ? Hitbox4.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Hitbox5 != null ? Hitbox5.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Hitbox6 != null ? Hitbox6.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(BaseMoveHitboxMetaDto left, BaseMoveHitboxMetaDto right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BaseMoveHitboxMetaDto left, BaseMoveHitboxMetaDto right)
        {
            return !Equals(left, right);
        }
    }
}

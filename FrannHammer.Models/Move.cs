using System;

namespace FrannHammer.Models
{
    public class BaseMoveModel
    {
        protected bool Equals(BaseMoveModel other)
        {
            return string.Equals(HitboxActive, other.HitboxActive) && string.Equals(FirstActionableFrame, other.FirstActionableFrame) && string.Equals(BaseDamage, other.BaseDamage) && string.Equals(Angle, other.Angle) && string.Equals(BaseKnockBackSetKnockback, other.BaseKnockBackSetKnockback) && string.Equals(LandingLag, other.LandingLag) && string.Equals(AutoCancel, other.AutoCancel) && string.Equals(KnockbackGrowth, other.KnockbackGrowth) && Type == other.Type && string.Equals(Name, other.Name) && OwnerId == other.OwnerId && Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as BaseMoveModel;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = HitboxActive != null ? HitboxActive.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (FirstActionableFrame != null ? FirstActionableFrame.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (BaseDamage != null ? BaseDamage.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Angle != null ? Angle.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (BaseKnockBackSetKnockback != null ? BaseKnockBackSetKnockback.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LandingLag != null ? LandingLag.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AutoCancel != null ? AutoCancel.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (KnockbackGrowth != null ? KnockbackGrowth.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)Type;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ OwnerId;
                hashCode = (hashCode * 397) ^ Id;
                return hashCode;
            }
        }

        public static bool operator ==(BaseMoveModel left, BaseMoveModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BaseMoveModel left, BaseMoveModel right)
        {
            return !Equals(left, right);
        }

        public string HitboxActive { get; set; }
        public string FirstActionableFrame { get; set; }
        public string BaseDamage { get; set; }
        public string Angle { get; set; }
        public string BaseKnockBackSetKnockback { get; set; }
        public string LandingLag { get; set; }
        public string AutoCancel { get; set; }
        public string KnockbackGrowth { get; set; }
        public MoveType Type { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public int Id { get; set; }
    }

    public class MoveDto : BaseMoveModel
    {
    }

    public class MoveSearchDto : BaseMoveModel
    {
        public CharacterDto Owner { get; set; }
    }

    public class Move : BaseMoveModel, IMoveEntity// : BaseModel
    {
        public DateTime LastModified { get; set; }

        //public override async Task<HttpResponseMessage> Create(HttpClient client)
        //{
        //    var httpResponseMessage = await client.PostAsJsonAsync($"{client.BaseAddress.AbsoluteUri}/moves", this);
        //    return httpResponseMessage;
        //}

        //public override Task<HttpResponseMessage> Update(HttpClient client)
        //{
        //    return client.PutAsJsonAsync($"{client.BaseAddress.AbsoluteUri}/moves/{Id}", this);
        //}

        //public override Task<HttpResponseMessage> Delete(HttpClient client)
        //{
        //    return client.DeleteAsync($"{client.BaseAddress.AbsoluteUri}/moves/{Id}");
        //}
    }
}
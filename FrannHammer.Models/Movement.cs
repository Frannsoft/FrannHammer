using System;

namespace FrannHammer.Models
{
    public class BaseMovementModel
    {
        protected bool Equals(BaseMovementModel other)
        {
            return string.Equals(Name, other.Name) && OwnerId == other.OwnerId && Id == other.Id && string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as BaseMovementModel;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ OwnerId;
                hashCode = (hashCode*397) ^ Id;
                hashCode = (hashCode*397) ^ (Value != null ? Value.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(BaseMovementModel left, BaseMovementModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BaseMovementModel left, BaseMovementModel right)
        {
            return !Equals(left, right);
        }

        public string Name { get; set; }
        public int OwnerId { get; set; }
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class MovementDto : BaseMovementModel
    { }

    public class Movement : BaseMovementModel// : BaseModel
    {
        public DateTime LastModified { get; set; }

        //public override async Task<HttpResponseMessage> Create(HttpClient client)
        //{
        //    var httpResponseMessage = await client.PostAsJsonAsync($"{client.BaseAddress.AbsoluteUri}/movements", this);
        //    return httpResponseMessage;
        //}

        //public override Task<HttpResponseMessage> Update(HttpClient client)
        //{
        //    return client.PutAsJsonAsync($"{client.BaseAddress.AbsoluteUri}/movements/{Id}", this);
        //}

        //public override Task<HttpResponseMessage> Delete(HttpClient client)
        //{
        //    return client.DeleteAsync($"{client.BaseAddress.AbsoluteUri}/movements");
        //}
    }
}
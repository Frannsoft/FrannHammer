using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrannHammer.Core.Models
{
    public abstract class BaseMeta : BaseModel
    {
        public int Id { get; set; }
        public Character Character { get; set; }
        public int CharacterId { get; set; }
        public Move Move { get; set; }
        public int MoveId { get; set; }
        public string Notes { get; set; }
        public DateTime LastModified { get; set; }

        public override Task<HttpResponseMessage> Update(HttpClient client)
        {
            throw new NotImplementedException("No support for updating these types of values through the API yet.  " +
                                              "This is a high priority");
        }

        public override Task<HttpResponseMessage> Delete(HttpClient client)
        {
            throw new NotImplementedException("No support for deleting these types of values through the API yet.  " +
                                              "This is a high priority");
        }

        public override Task<HttpResponseMessage> Create(HttpClient client)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class BaseMoveHitboxMeta : BaseMeta
    {
        public string Hitbox1 { get; set; }
        public string Hitbox2 { get; set; }
        public string Hitbox3 { get; set; }
        public string Hitbox4 { get; set; }
        public string Hitbox5 { get; set; }
    }

    public class Hitbox : BaseMoveHitboxMeta
    { }

    public class BaseDamage : BaseMoveHitboxMeta
    { }

    public class Angle : BaseMoveHitboxMeta
    { }

    public class BaseKnockbackSetKnockback : BaseMoveHitboxMeta
    { }

    public class KnockbackGrowth : BaseMoveHitboxMeta
    { }

    public class LandingLag : BaseMeta
    {
        public int Frames { get; set; }
    }

    public class Autocancel : BaseMeta
    {
        public string Cancel1 { get; set; }
        public string Cancel2 { get; set; }
    }
}
using System.Data.Common;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KuroganeHammer.Data.Api.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public ApplicationDbContext(DbConnection connection)
            : base(connection, true)
        { }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual System.Data.Entity.IDbSet<Character> Characters { get; set; }
        public virtual System.Data.Entity.IDbSet<CharacterAttribute> CharacterAttributes { get; set; }
        public virtual System.Data.Entity.IDbSet<Move> Moves { get; set; }
        public virtual System.Data.Entity.IDbSet<Movement> Movements { get; set; }
        public virtual System.Data.Entity.IDbSet<SmashAttributeType> SmashAttributeTypes { get; set; }
    }
}
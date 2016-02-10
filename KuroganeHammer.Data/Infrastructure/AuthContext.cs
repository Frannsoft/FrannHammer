using Microsoft.AspNet.Identity.EntityFramework;

namespace KuroganeHammer.Data.Infrastructure
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        { }
    }
}

using Microsoft.AspNet.Identity.EntityFramework;

namespace Kurogane.Data.RestApi
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        { }
    }
}

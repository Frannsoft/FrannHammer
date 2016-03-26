using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace Kurogane.Data.RestApi.Infrastructure
{
    public class AuthRepository : IDisposable
    {
        private readonly AuthContext _ctx;

        private readonly UserManager<ApplicationUser> _userManager;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }

        //public async Task<ApplicationUser> RegisterUser(UserModel userModel)
        //{
        //    ApplicationUser user = new ApplicationUser
        //    {
        //        UserName = userModel.UserName
        //    };

        //    var result = await _userManager.CreateAsync(user, userModel.Password);

        //    return result;
        //}

      
        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            var user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}

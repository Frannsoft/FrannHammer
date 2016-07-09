using System.Net.Http;
using System.Web.Http;
using FrannHammer.Api.Models;
using Microsoft.AspNet.Identity.Owin;

namespace FrannHammer.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        private ModelFactory _modelFactory;
        protected ModelFactory TheModelFactory => _modelFactory ?? (_modelFactory = new ModelFactory(Request, UserManager));

        protected readonly IApplicationDbContext Db = new ApplicationDbContext();

       
        protected BaseApiController(IApplicationDbContext context)
        {
            Db = context;    
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }

            base.Dispose(disposing);
        }
    }
}
using Kurogane.Data.RestApi.Infrastructure;
using Kurogane.Data.RestApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Kurogane.Data.RestApi.Controllers
{
    public class BaseApiController : ApiController
    {

        private ModelFactory _modelFactory;
        private ApplicationUserManager _AppUserManager = null;
        private ApplicationRoleManager _AppRoleManager = null;

        protected ApplicationUserManager AppUserManager
        {
            get
            {
                var manager = _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var provider = new DpapiDataProtectionProvider("KuroganeHammer");
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("usermanagement"));
                return manager;
            }
        }

        protected ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _AppRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        public BaseApiController()
        {
        }

        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(this.Request, this.AppUserManager);
                }
                return _modelFactory;
            }
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
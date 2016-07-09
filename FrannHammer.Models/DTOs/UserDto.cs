using System.Web.Http.Routing;

namespace FrannHammer.Models.DTOs
{
    public class UserDto
    {
        private readonly UrlHelper _urlHelper;

        //public string Url { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        //public bool EmailConfirmed { get; set; }
        //public IList<string> Roles { get; set; }
        //public IList<System.Security.Claims.Claim> Claims { get; set; }

        public UserDto(ApplicationUser appUser)
        {
            //_urlHelper = new UrlHelper();
            //Url = _urlHelper.Link("GetUser", new { id = appUser.Id });
            Id = appUser.Id;
            UserName = appUser.UserName;
            Email = appUser.Email;
            //EmailConfirmed = appUser.EmailConfirmed;
            //Roles = _appUserManager.GetRolesAsync(appUser.Id).Result;
            //Claims = _appUserManager.GetClaimsAsync(appUser.Id).Result;
        }

        public UserDto()
        { }
    }
}
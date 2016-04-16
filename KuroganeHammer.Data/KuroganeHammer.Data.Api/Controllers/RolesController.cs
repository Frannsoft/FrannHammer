using System;
using System.Threading.Tasks;
using System.Web.Http;
using KuroganeHammer.Data.Api.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using static KuroganeHammer.Data.Api.Models.RolesConstants;
using System.Linq;
using System.Web.Http.Description;

namespace KuroganeHammer.Data.Api.Controllers
{
    [Authorize(Roles = Admin)]
    [RoutePrefix("api/roles")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class RolesController : BaseApiController
    {

        [Route("{id:guid}", Name = "GetRoleById")]
        public async Task<IHttpActionResult> GetRole(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);

            if (role != null)
            {
                return Ok(TheModelFactory.Create(role));
            }

            return NotFound();
        }

        [Route("", Name = "GetAllRoles")]
        public IHttpActionResult GetAllRoles()
        {
            var roles = RoleManager.Roles.ToList();

            return Ok(roles);
        }

        [Route("create")]
        public async Task<IHttpActionResult> Create(CreateRoleBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = new IdentityRole { Name = model.Name };
            var result = await RoleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            var locationHeader = new Uri(Url.Link("GetRoleById", new { id = role.Id }));

            return Created(locationHeader, TheModelFactory.Create(role));

        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteRole(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);

            if (role != null)
            {
                var result = await RoleManager.DeleteAsync(role);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Ok();
            }

            return NotFound();
        }

        /// <summary>
        /// Add a user to a role via <see cref="UserToRoleModel"/>.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("AddUserToRole")]
        public async Task<IHttpActionResult> AddUserToRole(UserToRoleModel model)
        {
            var role = await RoleManager.FindByIdAsync(model.RoleId);

            if (role == null)
            {
                ModelState.AddModelError("", "Role does not exist");
                return BadRequest(ModelState);
            }

            var user = await UserManager.FindByIdAsync(model.UserId);

            if(!UserManager.IsInRole(user.Id, role.Name))
            {
                var result = await UserManager.AddToRoleAsync(user.Id, role.Name);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", $"User: {user} could not be added to role");
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        /// <summary>
        /// Removes a user from the role specified in the passed in <see cref="UserToRoleModel"/>.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("RemoveUserFromRole")]
        public async Task<IHttpActionResult> RemoveUserFromRole(UserToRoleModel model)
        {
            var role = await RoleManager.FindByIdAsync(model.RoleId);

            if (role == null)
            {
                ModelState.AddModelError("", "Role does not exist");
                return BadRequest(ModelState);
            }

            var user = await UserManager.FindByIdAsync(model.UserId);

            if (UserManager.IsInRole(user.Id, role.Name))
            {
                var result = await UserManager.RemoveFromRoleAsync(user.Id, role.Name);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", $"User: {user} could not be added to role");
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [Route("ManageUsersInRole")]
        public async Task<IHttpActionResult> ManageUsersInRole(UsersInRoleModel model)
        {
            var role = await RoleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ModelState.AddModelError("", "Role does not exist");
                return BadRequest(ModelState);
            }

            foreach (var user in model.EnrolledUsers)
            {
                var appUser = await UserManager.FindByIdAsync(user);

                if (appUser == null)
                {
                    ModelState.AddModelError("", $"User: {user} does not exists");
                    continue;
                }

                if (!UserManager.IsInRole(user, role.Name))
                {
                    var result = await UserManager.AddToRoleAsync(user, role.Name);

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", $"User: {user} could not be added to role");
                    }

                }
            }

            foreach (var user in model.RemovedUsers)
            {
                var appUser = await UserManager.FindByIdAsync(user);

                if (appUser == null)
                {
                    ModelState.AddModelError("", $"User: {user} does not exists");
                    continue;
                }

                var result = await UserManager.RemoveFromRoleAsync(user, role.Name);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", $"User: {user} could not be removed from role");
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
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
                    foreach (var error in result.Errors)
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
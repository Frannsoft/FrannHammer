using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FrannHammer.Api.Models;
using FrannHammer.Core;
using FrannHammer.Services;
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

        protected BaseApiController()
        { }

        /// <summary>
        /// Returns a content based response that is either a custom response or an
        /// existing DTO depending on the passed in fields.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="entity"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        protected IDictionary<string, object> BuildContentResponse<TEntity, TDto>(TEntity entity, string fields)
            where TEntity : class
        {
            return !string.IsNullOrEmpty(fields)
                ? new DtoBuilder().Build<TEntity, TDto>(entity, fields)
                : Mapper.Map<TEntity, TDto>(entity).GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(entity));
        }

        protected IHttpActionResult ReturnResponse(IDictionary<string, object> content)
        {
            if (content.Count > 0)
            {
                return Ok(content);
            }
            else
            {
                return NotFound();
            }
        }

        protected IHttpActionResult ReturnResponse(IList<IDictionary<string, object>> content)
        {
            if (content.Any())
            {
                return Ok(content);
            }
            else
            {
                return NotFound();
            }
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

        protected IEnumerable<IDictionary<string, object>> BuildContentResponseMultiple<TEntity, TDto>(IQueryable<TEntity> entities,
            string fields)
            where TEntity : class
            where TDto : class
        {
            var entitiesList = entities.ToList(); //note: this evaluates the result set fully!

            if (!string.IsNullOrEmpty(fields))
            {
                var builder = new DtoBuilder();
                return from entity in entitiesList
                       select builder.Build<TEntity, TDto>(entity, fields);
            }
            else
            {
                return from entity in entitiesList
                       select Mapper.Map<TEntity, TDto>(entity).GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(entity));
            }
        }
    }
}
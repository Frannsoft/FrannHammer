﻿using System.Net.Http;
using System.Web.Http;
using KuroganeHammer.Data.Api.Models;
using Microsoft.AspNet.Identity.Owin;

namespace KuroganeHammer.Data.Api.Controllers
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

        protected readonly ApplicationDbContext Db = new ApplicationDbContext();

        protected BaseApiController()
        { }

        protected BaseApiController(ApplicationDbContext context)
        {
            Db = context;    
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }

            base.Dispose(disposing);
        }
    }
}
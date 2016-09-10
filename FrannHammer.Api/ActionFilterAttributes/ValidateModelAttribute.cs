using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using FrannHammer.Services.Exceptions;

namespace FrannHammer.Api.ActionFilterAttributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null &&
                actionExecutedContext.Exception.GetType() == typeof(EntityNotFoundException))
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                    HttpStatusCode.NotFound, actionExecutedContext.Exception.Message);
            }
            else if (actionExecutedContext.Exception != null)
            {
                actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    actionExecutedContext.Exception.Message);
            }
        }
    }
}
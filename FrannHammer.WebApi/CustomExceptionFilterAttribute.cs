using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebApi
{
    /// <summary>
    /// Middleware for pushing exceptional data to the Application Insights instance.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is ResourceNotFoundException)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    actionExecutedContext.Exception.Message);
            }
            else
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    actionExecutedContext.Exception.Message);
            }
        }
    }
}
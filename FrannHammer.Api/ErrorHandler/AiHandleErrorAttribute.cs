using System;using System.Web.Mvc;using Microsoft.ApplicationInsights;namespace FrannHammer.Api.ErrorHandler{    /// <summary>
    /// Middleware for pushing exceptional data to the Application Insights instance.
    /// </summary>    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]     public class AiHandleErrorAttribute : HandleErrorAttribute    {        public override void OnException(ExceptionContext filterContext)        {            if (filterContext?.HttpContext != null && filterContext.Exception != null)            {                //If customError is Off, then AI HTTPModule will report the exception                if (filterContext.HttpContext.IsCustomErrorEnabled)                {                       var ai = new TelemetryClient();                    ai.TrackException(filterContext.Exception);                }             }            base.OnException(filterContext);        }    }}
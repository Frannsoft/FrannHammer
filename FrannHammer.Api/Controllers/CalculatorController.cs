using System;
using System.Web.Http;
using FrannHammer.Services;

namespace FrannHammer.Api.Controllers
{
    /// <summary>
    /// Controller used for calculating data off of character attributes and properties such as moves.
    /// </summary>
    [RoutePrefix("api")]
    [Obsolete("Others have created functionality in this area that is far more robust.  Support for this has been discontinued.")]
    public class CalculatorController : BaseApiController
    {
        private const string CalculatorRoutePrefix = "calculator";

        /// <summary>
        /// Creates a <see cref="CalculatorController"/> with the specified <see cref="ApplicationDbContext"/>.
        /// </summary>
        /// <param name="context"></param>
        public CalculatorController(ApplicationDbContext context)
            : base(context)
        { }
    }
}
using System.Net.Http;
using System.Web.Http.Filters;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.HypermediaServices;

namespace FrannHammer.WebApi.ActionFilterAttributes
{
    public class SingleCharacterResourceHalSupportAttribute : ActionFilterAttribute
    {
        public IEntityToBusinessTranslationService EntityToBusinessTranslationService { get; set; }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            var responseContent = actionExecutedContext.Response.Content as ObjectContent;

            var character = responseContent?.Value as ICharacter;

            if (character != null)
            {
                var characterResource =
                    EntityToBusinessTranslationService.ConvertToCharacterResourceWithHalSupport(character,
                        actionExecutedContext.Response.RequestMessage.GetUrlHelper());

                responseContent.Value = characterResource;
            }
        }
    }
}
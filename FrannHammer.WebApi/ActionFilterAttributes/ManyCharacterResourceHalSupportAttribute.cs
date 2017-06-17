using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Filters;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.HypermediaServices;
using FrannHammer.WebApi.Models;

namespace FrannHammer.WebApi.ActionFilterAttributes
{
    public class ManyCharacterResourceHalSupportAttribute : ActionFilterAttribute
    {
        public IEntityToBusinessTranslationService EntityToBusinessTranslationService { get; set; }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            var responseContent = actionExecutedContext.Response.Content as ObjectContent;

            var characters = responseContent?.Value as IEnumerable<ICharacter>;

            var characterResources = new List<CharacterResource>();

            if (characters != null)
            {
                foreach (var character in characters)
                {
                    var characterResource =
                        EntityToBusinessTranslationService.ConvertToCharacterResourceWithHalSupport(character,
                            actionExecutedContext.Response.RequestMessage.GetUrlHelper());

                    characterResources.Add(characterResource);
                }

                responseContent.Value = characterResources;
            }
        }
    }

    public class ManyMoveResourceHalSupportAttribute : ActionFilterAttribute
    {
        public IEntityToBusinessTranslationService EntityToBusinessTranslationService { get; set; }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            var responseContent = actionExecutedContext.Response.Content as ObjectContent;

            var moves = responseContent?.Value as IEnumerable<IMove>;

            var moveResources = new List<MoveResource>();

            if (moves != null)
            {
                foreach (var move in moves)
                {
                    var moveResource =
                        EntityToBusinessTranslationService.ConvertToMoveResourceWithHalSupport(move,
                            actionExecutedContext.Response.RequestMessage.GetUrlHelper());

                    moveResources.Add(moveResource);
                }

                responseContent.Value = moveResources;
            }
        }
    }
}
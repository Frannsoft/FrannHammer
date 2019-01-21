using AutoMapper;
using FrannHammer.NetCore.WebApi.Models;
using FrannHammer.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;

namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public abstract class ObjectContentResponseEnricher<TModel, TResource> : IResponseEnricher
    {
        protected internal ILinkProvider LinkProvider { get; }
        protected internal IMapper EntityToDtoMapper { get; }
        protected internal HttpContext Context { get; }
        protected internal LinkGenerator LinkGenerator { get; }

        protected ObjectContentResponseEnricher(ILinkProvider linkProvider, IMapper entityToDtoMapper, LinkGenerator linkGenerator,
            HttpContext context)
        {
            Guard.VerifyObjectNotNull(linkProvider, nameof(linkProvider));
            Guard.VerifyObjectNotNull(entityToDtoMapper, nameof(entityToDtoMapper));

            LinkProvider = linkProvider;
            EntityToDtoMapper = entityToDtoMapper;
            LinkGenerator = linkGenerator;
            Context = context;
        }

        public abstract TResource Enrich(TModel content);

        bool IResponseEnricher.CanEnrich(object content)
        {
            return content.GetType().FullName.Equals(typeof(TModel).FullName, StringComparison.OrdinalIgnoreCase);
            //return typeof(TModel).IsAssignableFrom(content.GetType());
        }

        //HttpResponse IResponseEnricher.Enrich(HttpResponse response)
        //{
        //    TModel content;
        //    if (response.TryGetContentValue(out content))
        //    {
        //        //since the api is returning the entity instead of a dto
        //        //we need to map/create a dto from the entity and add the links
        //        //Links only exist on the dto which is why the 
        //        //response content needs to be reassigned like this.  
        //        //This approach allows the api to return the 'pure' entity data
        //        //and not care about the web apis returned data.
        //        //Is this the way this is supposed to be done? (api returns entity and web api modifies as needed)
        //        var enrichedResource = Enrich(content, _linkGenerator);
        //        ((ObjectContent)response.Content).Value = enrichedResource;
        //    }

        //    return response;
        //}

        object IResponseEnricher.Enrich(string existingContent)
        {
            var content = JsonConvert.DeserializeObject<TModel>(existingContent);
            //TModel content = EntityToDtoMapper.Map<TModel>(tempDictionary);
            //if (response.TryGetContentValue(out content))
            //{
            //since the api is returning the entity instead of a dto
            //we need to map/create a dto from the entity and add the links
            //Links only exist on the dto which is why the 
            //response content needs to be reassigned like this.  
            //This approach allows the api to return the 'pure' entity data
            //and not care about the web apis returned data.
            //Is this the way this is supposed to be done? (api returns entity and web api modifies as needed)
            var enrichedResource = Enrich(content);
            //((ObjectContent)response.Content).Value = enrichedResource;
            //}

            return enrichedResource;
        }

        protected TLink CreateNameBasedLink<TLink>(string nameValue, string action, string controller)
            where TLink : Link
        {
            string hypermediaLink = LinkGenerator.GetPathByAction(Context, action, controller, new { name = nameValue });
            return CreateLinkCore<TLink>(hypermediaLink);
        }

        protected TLink CreateIdBasedLink<TLink>(string idValue, string action, string controller)
            where TLink : Link
        {
            string hypermediaLink = LinkGenerator.GetPathByAction(Context, action, controller, new { id = idValue });//urlHelper.Link(routeName, new { id = idValue });
            return CreateLinkCore<TLink>(hypermediaLink);
        }

        private TLink CreateLinkCore<TLink>(string rawHypermediaLink)
            where TLink : Link
        {
            string baseUrl = Context.Request.IsHttps ? "https://" : "http://";
            baseUrl += Context.Request.Host.Value;

            string queryParameterName = "game";

            string gameQueryParameter = $"?{queryParameterName}=";

            if (Context.Request.Query.ContainsKey(queryParameterName))
            {
                if (Context.Request.Query[queryParameterName].ToString().Equals("ultimate", StringComparison.OrdinalIgnoreCase))
                {
                    gameQueryParameter += "ultimate";
                }
                else
                {
                    gameQueryParameter += "smash4";
                }
            }
            else
            {
                gameQueryParameter += "smash4";
            }
            return LinkProvider.CreateLink<TLink>(baseUrl + rawHypermediaLink + gameQueryParameter);
        }
    }
}
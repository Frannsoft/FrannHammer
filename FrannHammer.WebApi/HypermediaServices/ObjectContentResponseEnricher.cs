﻿using System;
using System.Net.Http;
using System.Web.Http.Routing;
using AutoMapper;
using FrannHammer.Utility;
using FrannHammer.WebApi.Models;

namespace FrannHammer.WebApi.HypermediaServices
{
    public abstract class ObjectContentResponseEnricher<TModel, TResource> : IResponseEnricher
    {
        protected internal ILinkProvider LinkProvider { get; }
        protected internal IMapper EntityToDtoMapper { get; }

        protected ObjectContentResponseEnricher(ILinkProvider linkProvider, IMapper entityToDtoMapper)
        {
            Guard.VerifyObjectNotNull(linkProvider, nameof(linkProvider));
            Guard.VerifyObjectNotNull(entityToDtoMapper, nameof(entityToDtoMapper));

            LinkProvider = linkProvider;
            EntityToDtoMapper = entityToDtoMapper;
        }

        public virtual bool CanEnrich(Type contentType)
        {
            return contentType == typeof(TModel);
        }

        public abstract TResource Enrich(TModel content, UrlHelper urlHelper);

        bool IResponseEnricher.CanEnrich(HttpResponseMessage response)
        {
            var content = response.Content as ObjectContent;
            return content != null && CanEnrich(content.ObjectType);
        }

        HttpResponseMessage IResponseEnricher.Enrich(HttpResponseMessage response)
        {
            TModel content;
            if (response.TryGetContentValue(out content))
            {
                //since the api is returning the entity instead of a dto
                //we need to map/create a dto from the entity and add the links
                //Links only exist on the dto which is why the 
                //response content needs to be reassigned like this.  
                //This approach allows the api to return the 'pure' entity data
                //and not care about the web apis returned data.
                //Is this the way this is supposed to be done? (api returns entity and web api modifies as needed)
                var enrichedResource = Enrich(content, response.RequestMessage.GetUrlHelper());
                ((ObjectContent)response.Content).Value = enrichedResource;
            }

            return response;
        }

        protected TLink CreateNameBasedLink<TLink>(string nameValue, UrlHelper urlHelper, string routeName)
            where TLink : Link
        {
            string hypermediaLink = urlHelper.Link(routeName, new { name = nameValue });
            return CreateLinkCore<TLink>(hypermediaLink);
        }

        protected TLink CreateIdBasedLink<TLink>(string idValue, UrlHelper urlHelper, string routeName)
            where TLink : Link
        {
            string hypermediaLink = urlHelper.Link(routeName, new { id = idValue });
            return CreateLinkCore<TLink>(hypermediaLink);
        }

        private TLink CreateLinkCore<TLink>(string rawHypermediaLink)
            where TLink : Link
        {
            return LinkProvider.CreateLink<TLink>(rawHypermediaLink);
        }
    }
}
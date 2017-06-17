﻿using System.Collections.ObjectModel;
using System.Web.Http;
using FrannHammer.WebApi.HypermediaServices;

namespace FrannHammer.WebApi
{
    public static class ResponseEnricherConfigurationExtensions
    {
        public static void AddResponseEnrichers(this HttpConfiguration config, params IResponseEnricher[] enrichers)
        {
            foreach (var enricher in enrichers)
            {
                config.GetResponseEnrichers().Add(enricher);
            }
        }

        public static Collection<IResponseEnricher> GetResponseEnrichers(this HttpConfiguration config)
        {
            return (Collection<IResponseEnricher>)config.Properties.GetOrAdd(
                    typeof(Collection<IResponseEnricher>),
                    k => new Collection<IResponseEnricher>()
                );
        }
    }
}
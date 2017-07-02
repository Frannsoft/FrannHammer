using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FrannHammer.Domain;
using FrannHammer.Utility;

namespace FrannHammer.WebApi
{
    public class MongoDbBsonMapper
    {
        public void MapAllLoadedTypesDerivingFrom<T>(Assembly assemblyToReflectOver)
            where T : class
        {
            MapAllLoadedTypesDerivingFromCore<T>(assemblyToReflectOver);
        }

        public void MapAllLoadedTypesDerivingFrom<T>(IEnumerable<Assembly> assembliesToReflectOver)
            where T : class
        {
            foreach (var assembly in assembliesToReflectOver)
            {
                MapAllLoadedTypesDerivingFromCore<T>(assembly);
            } 
        }

        private static void MapAllLoadedTypesDerivingFromCore<T>(Assembly assemblyToReflectOver)
        {
            Guard.VerifyObjectNotNull(assemblyToReflectOver, nameof(assemblyToReflectOver));

            BsonMapper.RegisterClassMaps(GetModelTypes<T>().ToArray());
        }

        private static IEnumerable<Type> GetModelTypes<T>()
        {
            var modelTypes =
               Assembly.GetExecutingAssembly()
                   .GetReferencedAssemblies()
                   .SelectMany(an =>
                   {
                       return Assembly.Load(an).GetExportedTypes().Where(type => type.IsSubclassOf(typeof(T)));
                   });

            return modelTypes;
        }
    }
}
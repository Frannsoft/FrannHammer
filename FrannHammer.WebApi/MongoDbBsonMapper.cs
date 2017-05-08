using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FrannHammer.Domain;
using FrannHammer.Utility;
using MongoDB.Bson.Serialization;

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

            if (!IsTypeAlreadyRegistered(typeof(T)))
            {
                RegisterBaseType<T>();
            }

            RegisterModelTypes(GetModelTypes<T>());
        }

        private static void RegisterBaseType<T>()
        {
            BsonClassMap.RegisterClassMap<T>(m =>
            {
                m.AutoMap();
            });
        }

        private static bool IsTypeAlreadyRegistered(Type type) => BsonClassMap.IsClassMapRegistered(type);

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

        private static void RegisterModelTypes(IEnumerable<Type> modelTypes)
        {
            foreach (var modelType in modelTypes)
            {
                if (IsTypeAlreadyRegistered(modelType))
                { continue;}

                var classMap = new BsonClassMap(modelType);
                var properties =
                    modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                        .Where(p => p.GetCustomAttribute<FriendlyNameAttribute>() != null)
                        .ToList();

                foreach (var prop in properties)
                {
                    classMap.MapMember(prop).SetElementName(prop.GetCustomAttribute<FriendlyNameAttribute>().Name);
                }

                BsonClassMap.RegisterClassMap(classMap);
            }
        }
    }
}
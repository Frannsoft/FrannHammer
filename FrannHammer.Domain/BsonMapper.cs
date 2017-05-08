using System;
using System.Linq;
using System.Reflection;
using MongoDB.Bson.Serialization;

namespace FrannHammer.Domain
{
    public static class BsonMapper
    {
        public static void RegisterClassMaps(params Type[] types)
        {
            foreach (var type in types)
            {
                if (BsonClassMap.IsClassMapRegistered(type))
                { continue;}

                var classMap = new BsonClassMap(type);
                var properties =
                    type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                        .Where(p => p.GetCustomAttribute<FriendlyNameAttribute>() != null)
                        .ToList();

                foreach (var prop in properties)
                {
                    classMap.MapMember(prop).SetElementName(prop.GetCustomAttribute<FriendlyNameAttribute>().Name);
                }

                BsonClassMap.RegisterClassMap(classMap);
            }
        }

        public static void RegisterTypeWithAutoMap<T>()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(T)))
            {
                BsonClassMap.RegisterClassMap<T>(m =>
                {
                    m.AutoMap();
                });
            }
        }
    }
}

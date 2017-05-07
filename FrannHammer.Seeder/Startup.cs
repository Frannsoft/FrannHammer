using System;
using System.Linq;
using System.Reflection;
using FrannHammer.Domain;
using MongoDB.Bson.Serialization;

namespace FrannHammer.Seeder
{
    public static class Startup
    {
        public static void InitializeMapping()
        {
            BsonClassMap.RegisterClassMap<MongoModel>(m =>
            {
                m.AutoMap();
            });

            InitializeClassMap(typeof(Character));
            InitializeClassMap(typeof(Movement));
            InitializeClassMap(typeof(Move));
            InitializeClassMap(typeof(CharacterAttributeRow));
        }

        private static void InitializeClassMap(Type modelType)
        {
            var classMap = new BsonClassMap(modelType);
            var properties =
                modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                    .Where(p => p.GetCustomAttribute<FriendlyNameAttribute>(false) != null)
                    .ToList();

            classMap.AutoMap();

            foreach (var prop in properties)
            {
                classMap.MapMember(prop).SetElementName(prop.GetCustomAttribute<FriendlyNameAttribute>().Name);
            }

            BsonClassMap.RegisterClassMap(classMap);
        }
    }
}
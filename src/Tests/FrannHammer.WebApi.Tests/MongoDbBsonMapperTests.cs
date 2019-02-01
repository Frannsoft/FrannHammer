using FrannHammer.Domain;
using FrannHammer.NetCore.WebApi;
using MongoDB.Bson.Serialization;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FrannHammer.WebApi.Tests
{
    [TestFixture]
    public class MongoDbBsonMapperTests
    {
        [Test]
        public void ThrowsArgumentNullExceptionForNullAssemblyWhenMappingAllLoadedTypes()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new MongoDbBsonMapper().MapAllLoadedTypesDerivingFrom<BaseModel>(new List<Assembly> { null });
            });
        }

        [Test]
        public void RegistersTypesThatDeriveFromBaseType()
        {
            Type baseType = typeof(BaseModel);

            var sut = new MongoDbBsonMapper();

            var assemblyWithModelTypes = baseType.Assembly;

            var expectedRegisteredTypes = assemblyWithModelTypes
                    .GetExportedTypes()
                    .Where(type => type.IsSubclassOf(baseType))
                    .ToList();

            BsonClassMapHelper.Clear();

            //assert that types are not registered
            var preRegistrationClassMaps = BsonClassMap.GetRegisteredClassMaps();

            Assert.That(preRegistrationClassMaps.Count(), Is.EqualTo(0));

            //register types
            sut.MapAllLoadedTypesDerivingFrom<BaseModel>(assemblyWithModelTypes);

            //assert that types are registered now
            var actualRegisteredClassMapTypes = BsonClassMap.GetRegisteredClassMaps().Select(map => map.ClassType).ToList();

            Assert.That(actualRegisteredClassMapTypes.Count, Is.EqualTo(expectedRegisteredTypes.Count));
            Assert.That(actualRegisteredClassMapTypes.OrderBy(type => type.Name), Is.EqualTo(expectedRegisteredTypes.OrderBy(type => type.Name)));
        }
    }

    public static class BsonClassMapHelper
    {
        public static void Unregister<T>()
        {
            var classType = typeof(T);
            GetClassMap().Remove(classType);
        }

        static Dictionary<Type, BsonClassMap> GetClassMap()
        {
            var cm = BsonClassMap.GetRegisteredClassMaps().FirstOrDefault();
            if (cm != null)
            {
                var fi = typeof(BsonClassMap).GetField("__classMaps", BindingFlags.Static | BindingFlags.NonPublic);
                var classMaps = (Dictionary<Type, BsonClassMap>)fi.GetValue(cm);

                return classMaps;
            }
            else
            {
                return null;
            }
        }

        public static void Clear()
        {
            GetClassMap()?.Clear();
        }
    }
}

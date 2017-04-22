using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using FrannHammer.WebApi.Controllers;
using NUnit.Framework;

namespace FrannHammer.WebApi.Tests.Controllers
{
    [TestFixture]
    public class ControllerRoutingTests
    {
        private static IEnumerable<Type> ControllerTypes()
        {
            yield return typeof(CharacterAttributeController);
            yield return typeof(CharacterController);
            yield return typeof(MoveController);
        }

        [Test]
        [TestCaseSource(nameof(ControllerTypes))]
        public void RoutePrefixIsExpected(Type controllerType)
        {
            var routePrefixAttribute = controllerType.GetCustomAttribute<RoutePrefixAttribute>();

            Assert.That(routePrefixAttribute, Is.Not.Null,
                $"Unable to find route prefix attribute on controller of type '{controllerType.Name}'");

            string routePrefixValue = routePrefixAttribute.Prefix;

            Assert.That(routePrefixValue, Is.EqualTo("api"));
        }

        private static IEnumerable<Tuple<string, Type, string>> ControllerRouteInfo()
        {
            yield return Tuple.Create("characters/{id}", typeof(CharacterController), nameof(CharacterController.GetCharacter));
            yield return Tuple.Create("characters", typeof(CharacterController), nameof(CharacterController.GetAllCharacters));
            yield return Tuple.Create("characterattributes/{id}", typeof(CharacterAttributeController), nameof(CharacterAttributeController.GetCharacterAttribute));
            yield return Tuple.Create("characterattributes", typeof(CharacterAttributeController), nameof(CharacterAttributeController.GetCharacterAttributes));
            yield return Tuple.Create("moves/{id}", typeof(MoveController), nameof(MoveController.GetMove));
            yield return Tuple.Create("moves", typeof(MoveController), nameof(MoveController.GetAllMoves));
        }

        [Test]
        [TestCaseSource(nameof(ControllerRouteInfo))]
        public void ExpectedRouteNameIsUsed(Tuple<string, Type, string> controllerRouteInfo)
        {
            string expectedRoute = controllerRouteInfo.Item1;
            var controllerType = controllerRouteInfo.Item2;
            string methodName = controllerRouteInfo.Item3;

            var routeAttribute =
                controllerType.GetMethod(methodName)
                    .GetCustomAttribute<RouteAttribute>();

            Assert.That(routeAttribute, Is.Not.Null,
               $"Unable to find route attribute on controller of type '{controllerType.Name}' with method named '{methodName}");

            string routeValue = routeAttribute.Template;

            Assert.That(routeValue, Is.EqualTo(expectedRoute));
        }
    }
}

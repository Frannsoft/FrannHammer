﻿using System;
using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Core.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers.CharacterAttributeTypes
{
    [TestFixture]
    public class CharacterAttributeTypeControllerTest : EffortBaseTest
    {
        private CharacterAttributeTypesController _controller;

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _controller = new CharacterAttributeTypesController(Context);
        }

        [TestFixtureTearDown]
        public override void TestFixtureTearDown()
        {
            _controller.Dispose();
            base.TestFixtureTearDown();
        }

        [Test]
        public void ShouldGetCharacterAttributeType()
        {
            ExecuteAndReturnContent<CharacterAttributeType>(() => _controller.GetCharacterAttributeType(1));
        }

        [Test]
        public void ShouldGetAllCharacterAttributeTypes()
        {
            var characterAttributeTypes = _controller.GetCharacterAttributeTypes();
            CollectionAssert.AllItemsAreNotNull(characterAttributeTypes);
            CollectionAssert.AllItemsAreUnique(characterAttributeTypes);
            CollectionAssert.AllItemsAreInstancesOfType(characterAttributeTypes, typeof(CharacterAttributeType));
        }

        [Test]
        public void ShouldAddCharacterAttributeType()
        {
            var characterAttributeType = TestObjects.CharacterAttributeType();
            var result = ExecuteAndReturnCreatedAtRouteContent<CharacterAttributeType>(() => _controller.PostCharacterAttributeType(characterAttributeType));

            Assert.AreEqual(characterAttributeType, result);
        }

        [Test]
        public void ShouldUpdateCharacterAttributeType()
        {
            const string expectedName = "mewtwo";
            var characterAttributeType = TestObjects.CharacterAttributeType();

            var dateTime = DateTime.Now;

            //arrange
            var returnedCharacter =
                ExecuteAndReturnCreatedAtRouteContent<CharacterAttributeType>(() => _controller.PostCharacterAttributeType(characterAttributeType));
            //act
            if (returnedCharacter != null)
            {
                returnedCharacter.Name = expectedName;
                _controller.PutCharacterAttributeType(returnedCharacter.Id, returnedCharacter);
            }

            var updatedCharacter = ExecuteAndReturnContent<CharacterAttributeType>(() => _controller.GetCharacterAttributeType(characterAttributeType.Id));

            //assert
            Assert.That(updatedCharacter.Name, Is.EqualTo(expectedName));
            Assert.That(updatedCharacter.LastModified, Is.GreaterThan(dateTime));
        }

        [Test]
        public void ShouldDeleteCharacterAttributeType()
        {
            var characterAttribute = TestObjects.CharacterAttributeType();
            ExecuteAndReturnCreatedAtRouteContent<CharacterAttributeType>(() => _controller.PostCharacterAttributeType(characterAttribute));

            _controller.DeleteCharacterAttributeType(characterAttribute.Id);

            var characterAttributeController = new CharacterAttributesController(Context);
            ExecuteAndReturn<NotFoundResult>(() => characterAttributeController.GetCharacterAttribute(characterAttribute.Id));
        }
    }
}

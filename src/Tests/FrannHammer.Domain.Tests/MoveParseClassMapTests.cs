using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;

namespace FrannHammer.Domain.Tests
{
    [TestFixture]
    public class MoveParseClassMapTests
    {
        [TearDown]
        public void TearDown()
        {
            MoveParseClassMap.ClearAllRegisteredTypes();
        }

        [Test]
        public void RegistersExpectedTypeWhenCallingRegisterType()
        {
            MoveParseClassMap.RegisterType<IMove, Move>();

            var result = MoveParseClassMap.GetRegisteredImplementationTypeFor<IMove>();

            Assert.That(result, Is.EqualTo(typeof(Move)));
        }

        [Test]
        public void IgnoresMultipleRegistrationOfSameTypeToSameContract()
        {
            Assert.DoesNotThrow(() =>
            {
                MoveParseClassMap.RegisterType<IMove, Move>();
                MoveParseClassMap.RegisterType<IMove, Move>();
            });

            Assert.That(MoveParseClassMap.GetRegisteredImplementationTypeFor<IMove>, Is.EqualTo(typeof(Move)));
        }

        [Test]
        public void ThrowsInvalidOperationExceptionWhenRegisteringMoreThanOneType()
        {
            MoveParseClassMap.RegisterType<IMove, Move>();

            Assert.Throws<InvalidOperationException>(MoveParseClassMap.RegisterType<IMove, TestMove>);
        }

        [Test]
        public void ThrowsKeyNotFoundExceptionWhenNoTypeRegistered()
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                MoveParseClassMap.GetRegisteredImplementationTypeFor<IMove>();
            });
        }
    }

    public class TestMove : Move
    { }
}

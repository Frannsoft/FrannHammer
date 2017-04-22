using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using Moq;
using NUnit.Framework;

namespace FrannHammer.Api.Services.Tests
{
    [TestFixture]
    public class MoveServiceTests
    {
        [Test]
        public void AddSingleMove()
        {
            var fakeMoves = new List<IMove>
            {
                new Move {Name = "one"}
            };

            var moveRepositoryMock = new Mock<IRepository<IMove>>();
            moveRepositoryMock.Setup(c => c.GetAll()).Returns(() => fakeMoves);
            moveRepositoryMock.Setup(c => c.Add(It.IsAny<IMove>())).Callback<IMove>(c =>
            {
                fakeMoves.Add(c);
            });
            var service = new DefaultMoveService(moveRepositoryMock.Object);

            int previousCount = service.GetAll().Count();

            var newMove = new Move
            {
                Id = 999,
                Name = "two"
            };
            service.Add(newMove);

            int newCount = service.GetAll().Count();

            Assert.That(newCount, Is.EqualTo(previousCount + 1));
        }

        [Test]
        public void ReturnsNullForNoMoveFoundById()
        {
            var fakeMoves = new List<IMove>
            {
                new Move
                {
                    Id = 1,
                    Name = "one"
                }
            };

            var moveRepositoryMock = new Mock<IRepository<IMove>>();
            moveRepositoryMock.Setup(c => c.Get(It.IsAny<int>())).Returns<int>(id => fakeMoves.FirstOrDefault(c => c.Id == id));

            var service = new DefaultMoveService(moveRepositoryMock.Object);

            var move = service.Get(0);

            Assert.That(move, Is.Null);
        }

        [Test]
        public void Error_RejectsNullRepositoryInConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new DefaultMoveService(null);
            });
        }

        [Test]
        public void Error_RejectsNullMoveForAddition()
        {
            var moveRepositoryMock = new Mock<IRepository<IMove>>();

            Assert.Throws<ArgumentNullException>(() =>
            {
                var service = new DefaultMoveService(moveRepositoryMock.Object);

                service.Add(null);
            });
        }
    }
}

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
    public class MovementmentServiceTests
    {
        [Test]
        public void AddSingleMovement()
        {
            var fakeMovements = new List<IMovement>
            {
                new Movement {Name = "one"}
            };

            var movementRepositoryMock = new Mock<IRepository<IMovement>>();
            movementRepositoryMock.Setup(c => c.GetAll()).Returns(() => fakeMovements);
            movementRepositoryMock.Setup(c => c.Add(It.IsAny<IMovement>())).Callback<IMovement>(c =>
            {
                fakeMovements.Add(c);
            });
            var service = new DefaultMovementService(movementRepositoryMock.Object);

            int previousCount = service.GetAll().Count();

            var newMovement = new Movement
            {
                Id = 999,
                Name = "two"
            };
            service.Add(newMovement);

            int newCount = service.GetAll().Count();

            Assert.That(newCount, Is.EqualTo(previousCount + 1));
        }

        [Test]
        public void ReturnsNullForNoMovementFoundById()
        {
            var fakeMovements = new List<IMovement>
            {
                new Movement
                {
                    Id = 1,
                    Name = "one"
                }
            };

            var movementRepositoryMock = new Mock<IRepository<IMovement>>();
            movementRepositoryMock.Setup(c => c.Get(It.IsAny<int>())).Returns<int>(id => fakeMovements.FirstOrDefault(c => c.Id == id));

            var service = new DefaultMovementService(movementRepositoryMock.Object);

            var movement = service.Get(0);

            Assert.That(movement, Is.Null);
        }

        [Test]
        public void Error_RejectsNullRepositoryInConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new DefaultMovementService(null);
            });
        }

        [Test]
        public void Error_RejectsNullMovementForAddition()
        {
            var movementRepositoryMock = new Mock<IRepository<IMovement>>();

            Assert.Throws<ArgumentNullException>(() =>
            {
                var service = new DefaultMovementService(movementRepositoryMock.Object);

                service.Add(null);
            });
        }
    }
}

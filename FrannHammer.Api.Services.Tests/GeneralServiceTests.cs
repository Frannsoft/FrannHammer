using System;
using System.Linq;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace FrannHammer.Api.Services.Tests
{
    [TestFixture(typeof(IMove), typeof(Move), typeof(DefaultMoveService))]
    [TestFixture(typeof(IMovement), typeof(Movement), typeof(DefaultMovementService))]
    [TestFixture(typeof(ICharacter), typeof(Character), typeof(DefaultCharacterService))]
    [TestFixture(typeof(ICharacterAttributeRow), typeof(CharacterAttributeRow), typeof(DefaultCharacterAttributeService))]
    public class GeneralServiceTests<TModelInterface, TModel, TSut>
        where TModelInterface : class, IModel
        where TModel : TModelInterface
        where TSut : ICrudService<TModelInterface>
    {
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        [Test]
        public void CanAddSingleItemToRepository()
        {
            var repositoryItems = _fixture.CreateMany<TModelInterface>().ToList();

            var repositoryMock = new Mock<IRepository<TModelInterface>>();
            repositoryMock.Setup(c => c.GetAll()).Returns(() => repositoryItems);
            repositoryMock.Setup(c => c.Add(It.IsAny<TModelInterface>())).Callback<TModelInterface>(c =>
            {
                repositoryItems.Add(c);
            });
            var sut = CreateCrudService(repositoryMock.Object);

            int previousCount = sut.GetAll().Count();

            var newItem = _fixture.Create<TModelInterface>();

            sut.Add(newItem);

            int newCount = sut.GetAll().Count();

            Assert.That(newCount, Is.EqualTo(previousCount + 1));
        }

        [Test]
        public void GetItemByNameCallsRepositoryGetSingleWhereNameIsMethod()
        {
            var items = _fixture.CreateMany<TModel>().ToList();
            string name = items.First().Name;

            var repositoryMock = new Mock<IRepository<TModelInterface>>();
            repositoryMock.Setup(r => r.GetSingleWhere(It.IsAny<Func<TModelInterface, bool>>())).Returns((Func<TModel, bool> where) => items.Single(where));

            var sut = CreateCrudService(repositoryMock.Object);

            sut.GetSingleWhere(t => t.Name == name);

            repositoryMock.VerifyAll();
        }

        [Test]
        public void ReturnsNullForNoItemFoundById()
        {
            var items = _fixture.CreateMany<TModel>().ToList();

            var repositoryMock = new Mock<IRepository<TModelInterface>>();
            repositoryMock.Setup(c => c.GetSingleWhere(It.IsAny<Func<TModelInterface, bool>>())).Returns((Func<TModel, bool> where) => items.SingleOrDefault(where));

            var sut = CreateCrudService(repositoryMock.Object);

            var item = sut.GetSingleById("0");

            Assert.That(item, Is.Null);
        }

        [Test]
        public void RejectsNullItemForAddition()
        {
            var repositoryMock = new Mock<IRepository<TModelInterface>>();

            Assert.Throws<ArgumentNullException>(() =>
            {
                var sut = CreateCrudService(repositoryMock.Object);
                sut.Add(null);
            });
        }

        private static ICrudService<TModelInterface> CreateCrudService(IRepository<TModelInterface> repository)
        {
            return (ICrudService<TModelInterface>)Activator.CreateInstance(typeof(TSut), repository);
        }
    }
}

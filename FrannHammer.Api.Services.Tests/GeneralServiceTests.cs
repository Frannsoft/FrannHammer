using System;
using System.Linq;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace FrannHammer.Api.Services.Tests
{
    [TestFixture(typeof(IMove), typeof(DefaultMoveService))]
    [TestFixture(typeof(IMovement), typeof(DefaultMovementService))]
    [TestFixture(typeof(ICharacter), typeof(DefaultCharacterService))]
    [TestFixture(typeof(ICharacterAttributeRow), typeof(DefaultCharacterAttributeService))]
    public class GeneralServiceTests<TModel, TSut>
        where TModel : class, IModel
        where TSut : ICrudService<TModel>
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
            var repositoryItems = _fixture.CreateMany<TModel>().ToList();

            var repositoryMock = new Mock<IRepository<TModel>>();
            repositoryMock.Setup(c => c.GetAll()).Returns(() => repositoryItems);
            repositoryMock.Setup(c => c.Add(It.IsAny<TModel>())).Callback<TModel>(c =>
            {
                repositoryItems.Add(c);
            });
            var sut = CreateCrudServiceSut(repositoryMock.Object);

            int previousCount = sut.GetAll().Count();

            var newItem = _fixture.Create<TModel>();

            sut.Add(newItem);

            int newCount = sut.GetAll().Count();

            Assert.That(newCount, Is.EqualTo(previousCount + 1));
        }

        [Test]
        public void ReturnsNullForNoItemFoundById()
        {
            var items = _fixture.CreateMany<TModel>().ToList();

            var repositoryMock = new Mock<IRepository<TModel>>();
            repositoryMock.Setup(c => c.Get(It.IsAny<string>())).Returns<string>(id => items.FirstOrDefault(c => c.Id == id.ToString()));

            var sut = CreateCrudServiceSut(repositoryMock.Object);

            var item = sut.Get("0");

            Assert.That(item, Is.Null);
        }

     

        [Test]
        public void RejectsNullItemForAddition()
        {
            var repositoryMock = new Mock<IRepository<TModel>>();

            Assert.Throws<ArgumentNullException>(() =>
            {
                var sut = CreateCrudServiceSut(repositoryMock.Object);
                sut.Add(null);
            });
        }

        private static ICrudService<TModel> CreateCrudServiceSut(IRepository<TModel> repository)
        {
            return (ICrudService<TModel>)Activator.CreateInstance(typeof(TSut), repository);
        }
    }
}

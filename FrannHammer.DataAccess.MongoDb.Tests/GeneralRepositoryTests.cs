using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace FrannHammer.DataAccess.MongoDb.Tests
{
    [TestFixture(typeof(ICharacter), typeof(Character))]
    [TestFixture(typeof(IMovement), typeof(Movement))]
    [TestFixture(typeof(IMove), typeof(Move))]
    [TestFixture(typeof(ICharacterAttributeRow), typeof(CharacterAttributeRow))]
    public class GeneralRepositoryTests<TModelInterface, TModel> : BaseRepositoryTests
        where TModelInterface : class, IModel
        where TModel : class, TModelInterface
    {
        private Fixture _fixture;
        private IRepository<TModelInterface> _repository;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            //_fixture.Customize(new AutoMoqCustomization());
        }

        [Test]
        public void GetSingleObjectById()
        {
            _repository = new MongoDbRepository<TModelInterface>(MongoDatabase);

            var newlyAddedCharacter = _repository.Add(_fixture.Create<TModel>());

            var character = _repository.GetById(newlyAddedCharacter.Id);
            Assert.That(character, Is.Not.Null);
        }
    }
}

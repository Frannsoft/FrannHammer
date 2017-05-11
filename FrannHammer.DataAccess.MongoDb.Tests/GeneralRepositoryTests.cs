using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace FrannHammer.DataAccess.MongoDb.Tests
{
    //Commenting these out for now.  Setting up generic tests that have multi-layered Interface properties (character attribute row's IAttribute Value property) 
    //is proving to take quite some time to get just a little benefit since these tests are so basic.

    //[TestFixture(typeof(ICharacter), typeof(Character))]
    //[TestFixture(typeof(IMovement), typeof(Movement))]
    //[TestFixture(typeof(IMove), typeof(Move))]
    //[TestFixture(typeof(ICharacterAttributeRow), typeof(CharacterAttributeRow))]
    //public class GeneralRepositoryTests<TModelInterface, TModel> : BaseRepositoryTests
    //    where TModelInterface : class, IModel
    //    where TModel : class, TModelInterface
    //{
    //    private Fixture _fixture;
    //    private IRepository<TModelInterface> _repository;

    //    [SetUp]
    //    public void SetUp()
    //    {
    //        _fixture = new Fixture();
    //    }

    //    [Test]
    //    public void GetSingleObjectById()
    //    {
    //        _repository = new MongoDbRepository<TModelInterface>(MongoDatabase);

    //        var newlyAddedCharacter = _repository.Add(_fixture.Create<TModel>());

    //        var character = _repository.GetById(newlyAddedCharacter.Id);
    //        Assert.That(character, Is.Not.Null);
    //    }
    //}
}

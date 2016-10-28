using System.Linq;
using FrannHammer.Models;
using FrannHammer.Services.Tests.Harnesses;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public class MetadataServiceSearchTest : ServiceBaseTest
    {
        private IMetadataService _metadataService;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _metadataService = new MetadataService(Context, ResultValidationService);
        }

        [Test]
        public void ReturnsNameOnlyResult()
        {
            var model = new ComplexMoveSearchModel
            {
                Name = "jab"
            };

            var results = _metadataService.GetAll<MoveDto>(model).ToList();

            Assert.That(results, Is.Not.Null);
            Assert.That(results.Count, Is.GreaterThan(0));
            HarnessAsserts.ExpandoObjectIsCorrect(results, $"{Id},{Name}");
        }
    }
}

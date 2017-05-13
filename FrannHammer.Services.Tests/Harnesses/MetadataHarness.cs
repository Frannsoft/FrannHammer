using System.Collections.Generic;
using System.Linq;
using FrannHammer.Models;
using FrannHammer.Services.MoveSearch;
using FrannHammer.Services.Tests.Suts;
using NUnit.Framework;

namespace FrannHammer.Services.Tests.Harnesses
{
    public class MetadataHarness<T, TDto> : IMetadataHarness
        where T : class, IEntity
        where TDto : class
    {
        private readonly IResultValidationService _resultValidationService;

        private readonly MetadataSut<T, TDto> _metadataSut;
        public string Fields { get; }

        public MetadataHarness(string fields = "")
        {
            _resultValidationService = new ResultValidationService();
            _metadataSut = new MetadataSut<T, TDto>();
            Fields = fields;
        }

        public void SingleIsValidMeta(int id, IApplicationDbContext context)
        {
            var item = _metadataSut.Get(id, context, _resultValidationService);

            Assert.That(item, Is.Not.Null);
        }

        public void SingleIsValidMetaWithFields(int id, IApplicationDbContext context, string fields)
        {
            var item = _metadataSut.Get(id, context, _resultValidationService, fields);

            Assert.That(item, Is.Not.Null);

            if (!string.IsNullOrEmpty(fields))
            {
                HarnessAsserts.ExpandoObjectIsCorrect(item, fields);
            }
        }

        public void CollectionIsValidWithFields(IApplicationDbContext context, string fields = "")
        {
            var items = _metadataSut.GetAll(context, _resultValidationService, fields).ToList();

            Assert.That(items, Is.TypeOf<List<dynamic>>());

            if (!string.IsNullOrEmpty(fields))
            {
                HarnessAsserts.ExpandoObjectIsCorrect(items, fields);
            }
        }

        public void CollectionIsValid(IApplicationDbContext context)
        {
            var items = _metadataSut.GetAll(context, _resultValidationService);

            CollectionAssert.AllItemsAreNotNull(items);
        }

       
    }
}

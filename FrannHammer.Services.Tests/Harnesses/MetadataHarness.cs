﻿using System.Collections.Generic;
using System.Linq;
using FrannHammer.Models;
using FrannHammer.Services.Tests.Suts;
using NUnit.Framework;

namespace FrannHammer.Services.Tests.Harnesses
{
    public class MetadataHarness<T, TDto> : IMetadataHarness
        where T : class, IEntity
        where TDto : class
    {
        private readonly MetadataSut<T, TDto> _metadataSut;
        public string Fields { get; }

        public MetadataHarness(string fields = "")
        {
            _metadataSut = new MetadataSut<T, TDto>();
            Fields = fields;
        }

        public void SingleIsValidMeta(int id, IApplicationDbContext context)
        {
            var item = _metadataSut.Get(id, context);

            Assert.That(item, Is.Not.Null);
        }

        public void SingleIsValidMetaWithFields(int id, IApplicationDbContext context, string fields)
        {
            var item = _metadataSut.Get(id, context, fields);

            Assert.That(item, Is.Not.Null);

            if (!string.IsNullOrEmpty(fields))
            {
                HarnessAsserts.ExpandoObjectIsCorrect(item, fields);
            }
        }

        public void CollectionIsValidWithFields(IApplicationDbContext context, string fields = "")
        {
            var items = _metadataSut.GetAll(context, fields).ToList();

            CollectionAssert.IsNotEmpty(items);
            CollectionAssert.AllItemsAreNotNull(items);
            CollectionAssert.AllItemsAreUnique(items);

            if (!string.IsNullOrEmpty(fields))
            {
                HarnessAsserts.ExpandoObjectIsCorrect(items, fields);
            }
        }

        public void CollectionIsValid(IApplicationDbContext context)
        {
            var items = _metadataSut.GetAll(context).ToList();

            CollectionAssert.IsNotEmpty(items);
            CollectionAssert.AllItemsAreNotNull(items);
            CollectionAssert.AllItemsAreUnique(items);
        }
    }
}

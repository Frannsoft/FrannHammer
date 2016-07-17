using System.Collections.Generic;
using System.Linq;
using FrannHammer.Models;
using FrannHammer.Services.Tests.Suts;
using NUnit.Framework;

namespace FrannHammer.Services.Tests.Harnesses
{
    public class MoveDataHarness<T, TDto> : IMoveDataHarness
        where T : class, IMoveIdEntity
        where TDto : class
    {
        public string Fields { get; }

        private readonly MoveDataSut<T, TDto> _movedataSut;

        public MoveDataHarness(string fields = "")
        {
            _movedataSut = new MoveDataSut<T, TDto>();
            Fields = fields;
        }

        public void SingleIsValidMove(int id, IApplicationDbContext context)
        {
            var item = _movedataSut.GetWithMoves(id, context);

            Assert.That(item, Is.Not.Null);
        }

        public void SingleIsValidMoveWithFields(int id, IApplicationDbContext context, string fields)
        {
            var item = _movedataSut.GetWithMoves(id, context, fields);

            Assert.That(item, Is.Not.Null);

            if (!string.IsNullOrEmpty(fields))
            {
                HarnessAsserts.ExpandoObjectIsCorrect(item, fields);
            }
        }

        public void CollectionIsValidWithFields(IApplicationDbContext context, string fields = "")
        {
            var items = _movedataSut.GetAllWithMoves(context, fields).ToList();

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
            var items = _movedataSut.GetAllWithMoves(context).ToList();

            CollectionAssert.IsNotEmpty(items);
            CollectionAssert.AllItemsAreNotNull(items);
            CollectionAssert.AllItemsAreUnique(items);
        }
    }



}

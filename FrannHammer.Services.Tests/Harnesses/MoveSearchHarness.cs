using System;
using System.Linq;
using FrannHammer.Models;
using FrannHammer.Services.MoveSearch;
using FrannHammer.Services.Tests.Suts;
using NUnit.Framework;

namespace FrannHammer.Services.Tests.Harnesses
{
    public interface IMoveSearchHarness
    {
        string Fields { get; }

        void SearchResultCollectionIsValid<TMoveType>(MoveSearchModel searchModel,
            IApplicationDbContext context, Func<TMoveType, bool> serviceSearchMethod)
            where TMoveType : class, IMoveIdEntity;
    }

    public class MoveSearchHarness : IMoveSearchHarness
    {
        private readonly IResultValidationService _resultValidationService;
        private readonly MoveSearchSut _moveSearchSut;
        public string Fields { get; }

        public MoveSearchHarness(string fields = "")
        {
            _resultValidationService = new ResultValidationService();
            _moveSearchSut = new MoveSearchSut();
            Fields = fields;
        }

        public void SearchResultCollectionIsValid<TMoveType>(MoveSearchModel searchModel,
           IApplicationDbContext context, Func<TMoveType, bool> serviceSearchMethod)
           where TMoveType : class, IMoveIdEntity
        {
            var results = _moveSearchSut.GetAll(searchModel, context, _resultValidationService, Fields).ToList();

            Assert.That(results, Is.Not.Null);
            Assert.That(results.Count, Is.GreaterThan(0));

            if (!string.IsNullOrEmpty(Fields))
            {
                HarnessAsserts.ExpandoObjectIsCorrect(results, Fields);
            }

            foreach (var result in results)
            {
                int moveId = result.Id;
                string moveName = result.Name;
                var thisMovesData = context.Set<TMoveType>().Where(h => h.MoveId == moveId);

                foreach (var data in thisMovesData)
                {
                    Assert.That(serviceSearchMethod(data), $"Matching data of type {typeof(TMoveType).Name} " +
                                                           $"for move {moveName} were not able to be found!");
                }
            }
        }
    }
}

using System;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class SearchPredicateFactory
    {
        private readonly HitboxStartupSearchPredicateService _hitboxSearchExpressionService;
        private readonly HitboxActiveSearchPredicateService _hitboxActiveSearchPredicateService;
        private readonly NameSearchPredicateService _nameSearchExpressionService;

        public SearchPredicateFactory()
        {
            _hitboxSearchExpressionService = new HitboxStartupSearchPredicateService();
            _nameSearchExpressionService = new NameSearchPredicateService();
            _hitboxActiveSearchPredicateService = new HitboxActiveSearchPredicateService();
        }

        public Func<Hitbox, bool> CreateHitboxStartupPredicate(ComplexMoveSearchModel searchModel)
            =>
            _hitboxSearchExpressionService.GetHitboxStartupPredicate(searchModel.HitboxStartupFrame);

        public Func<Hitbox, bool> CreateHitboxActiveOnFramePredicate(ComplexMoveSearchModel searchModel)
            => _hitboxActiveSearchPredicateService.GetHitboxActivePredicate(searchModel.HitboxActiveOnFrame);

        public Func<Move, bool> CreateNamePredicate(ComplexMoveSearchModel searchModel)
            => _nameSearchExpressionService.GetNameDelegate(searchModel.Name);
    }
}

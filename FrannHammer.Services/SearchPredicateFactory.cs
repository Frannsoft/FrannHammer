using System;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class SearchPredicateFactory
    {
        private readonly HitboxStartupSearchPredicateService _hitboxSearchExpressionService;
        private readonly HitboxActiveSearchPredicateService _hitboxActiveSearchPredicateService;
        private readonly NameSearchPredicateService _nameSearchExpressionService;
        private readonly FirstActionableFrameSearchPredicateService _firstActionableFrameSearchPredicateService;

        public SearchPredicateFactory()
        {
            _hitboxSearchExpressionService = new HitboxStartupSearchPredicateService();
            _nameSearchExpressionService = new NameSearchPredicateService();
            _hitboxActiveSearchPredicateService = new HitboxActiveSearchPredicateService();
            _firstActionableFrameSearchPredicateService = new FirstActionableFrameSearchPredicateService();
        }

        public Func<Hitbox, bool> CreateHitboxStartupPredicate(ComplexMoveSearchModel searchModel)
            =>
            _hitboxSearchExpressionService.GetHitboxStartupPredicate(searchModel.HitboxStartupFrame);

        public Func<Hitbox, bool> CreateHitboxActiveOnFramePredicate(ComplexMoveSearchModel searchModel)
            => _hitboxActiveSearchPredicateService.GetHitboxActivePredicate(searchModel.HitboxActiveOnFrame);

        public Func<Move, bool> CreateNamePredicate(ComplexMoveSearchModel searchModel)
            => _nameSearchExpressionService.GetNameDelegate(searchModel.Name);

        public Func<Move, bool> CreateFirstActionableFramePredicate(ComplexMoveSearchModel searchModel)
            => _firstActionableFrameSearchPredicateService.GetFirstActionableFrameSearchPredicate(searchModel.FirstActionableFrame);
    }
}

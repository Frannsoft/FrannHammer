using System;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class SearchPredicateFactory
    {
        private readonly HitboxStartupSearchPredicateService _hitboxSearchPredicateService;
        private readonly BaseDamageSearchPredicateService _baseDamagePredicateService;
        private readonly HitboxActiveSearchPredicateService _hitboxActiveSearchPredicateService;
        private readonly NameSearchPredicateService _nameSearchExpressionService;
        private readonly FirstActionableFrameSearchPredicateService _firstActionableFrameSearchPredicateService;

        private readonly SearchPredicateService _searchPredicateService;

        public SearchPredicateFactory()
        {
            _hitboxSearchPredicateService = new HitboxStartupSearchPredicateService();
            _baseDamagePredicateService = new BaseDamageSearchPredicateService();
            _nameSearchExpressionService = new NameSearchPredicateService();
            _hitboxActiveSearchPredicateService = new HitboxActiveSearchPredicateService();
            _firstActionableFrameSearchPredicateService = new FirstActionableFrameSearchPredicateService();
            _searchPredicateService = new SearchPredicateService();
        }

        public Func<Hitbox, bool> CreateHitboxStartupPredicate(ComplexMoveSearchModel searchModel)
            => _searchPredicateService.GetPredicate<Hitbox>(searchModel.HitboxStartupFrame);

        public Func<BaseDamage, bool> CreateBaseDamagePredicate(ComplexMoveSearchModel searchModel)
            => _searchPredicateService.GetPredicate<BaseDamage>(searchModel.BaseDamage);

        public Func<Angle, bool> CreateAnglePredicate(ComplexMoveSearchModel searchModel)
            => _searchPredicateService.GetPredicate<Angle>(searchModel.Angle);

        public Func<Hitbox, bool> CreateHitboxActiveOnFramePredicate(ComplexMoveSearchModel searchModel)
            => _hitboxActiveSearchPredicateService.GetHitboxActivePredicate(searchModel.HitboxActiveOnFrame);

        public Func<Move, bool> CreateFirstActionableFramePredicate(ComplexMoveSearchModel searchModel)
           => _firstActionableFrameSearchPredicateService.GetFirstActionableFrameSearchPredicate(searchModel.FirstActionableFrame);

        public Func<Move, bool> CreateNamePredicate(ComplexMoveSearchModel searchModel)
            => _nameSearchExpressionService.GetNameDelegate(searchModel.Name);

       
    }
}

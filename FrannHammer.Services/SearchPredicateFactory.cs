using System;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class SearchPredicateFactory
    {
        //these have custom range checks
        private readonly HitboxActiveSearchPredicateService _hitboxActiveSearchPredicateService;
        private readonly NameSearchPredicateService _nameSearchExpressionService;
        private readonly FirstActionableFrameSearchPredicateService _firstActionableFrameSearchPredicateService;

        //uses default range checks
        private readonly SearchPredicateService _baseMoveHitboxPredicateService;

        public SearchPredicateFactory()
        {
            _nameSearchExpressionService = new NameSearchPredicateService();
            _hitboxActiveSearchPredicateService = new HitboxActiveSearchPredicateService();
            _firstActionableFrameSearchPredicateService = new FirstActionableFrameSearchPredicateService();
            _baseMoveHitboxPredicateService = new SearchPredicateService();
        }

        public Func<Hitbox, bool> CreateHitboxStartupPredicate(ComplexMoveSearchModel searchModel)
            => _baseMoveHitboxPredicateService.GetPredicate<Hitbox>(searchModel.HitboxStartupFrame);

        public Func<BaseDamage, bool> CreateBaseDamagePredicate(ComplexMoveSearchModel searchModel)
            => _baseMoveHitboxPredicateService.GetPredicate<BaseDamage>(searchModel.BaseDamage);

        public Func<Angle, bool> CreateAnglePredicate(ComplexMoveSearchModel searchModel)
            => _baseMoveHitboxPredicateService.GetPredicate<Angle>(searchModel.Angle);

        public Func<BaseKnockback, bool> CreateBaseKnockbackPredicate(ComplexMoveSearchModel searchModel)
            => _baseMoveHitboxPredicateService.GetPredicate<BaseKnockback>(searchModel.BaseKnockback);

        public Func<SetKnockback, bool> CreateSetKnockbackPredicate(ComplexMoveSearchModel searchModel)
           => _baseMoveHitboxPredicateService.GetPredicate<SetKnockback>(searchModel.SetKnockback);

        public Func<KnockbackGrowth, bool> CreateKnockbackGrowthPredicate(ComplexMoveSearchModel searchModel)
           => _baseMoveHitboxPredicateService.GetPredicate<KnockbackGrowth>(searchModel.KnockbackGrowth);

        public Func<Hitbox, bool> CreateHitboxActiveOnFramePredicate(ComplexMoveSearchModel searchModel)
            => _hitboxActiveSearchPredicateService.GetHitboxActivePredicate(searchModel.HitboxActiveOnFrame);

        public Func<Move, bool> CreateFirstActionableFramePredicate(ComplexMoveSearchModel searchModel)
           => _firstActionableFrameSearchPredicateService.GetFirstActionableFrameSearchPredicate(searchModel.FirstActionableFrame);

        public Func<Move, bool> CreateNamePredicate(ComplexMoveSearchModel searchModel)
            => _nameSearchExpressionService.GetNameDelegate(searchModel.Name);

       
    }
}

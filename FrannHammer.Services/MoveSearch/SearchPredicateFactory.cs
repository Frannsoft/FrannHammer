using System;
using FrannHammer.Models;

namespace FrannHammer.Services.MoveSearch
{
    public class SearchPredicateFactory
    {
        //these have custom range checks
        private readonly HitboxActiveSearchPredicateService _hitboxActiveSearchPredicateService;
        private readonly HitboxActiveLengthSearchPredicateService _hitboxActiveLengthSearchPredicateService;
        private readonly NameSearchPredicateService _nameSearchPredicateService;
        private readonly CharacterNameSearchPredicateService _characterNameSearchPredicateService;
        private readonly FirstActionableFrameSearchPredicateService _firstActionableFrameSearchPredicateService;
        private readonly LandingLagSearchPredicateService _landingLagSearchPredicateService;
        private readonly AutoCancelSearchPredicateService _autocancelSearchPredicateService;

        //uses default range checks
        private readonly SearchPredicateService _baseMoveHitboxPredicateService;

        public Func<Hitbox, bool> HitboxActiveLengthPredicate { get; private set; }
        public Func<Hitbox, bool> HitboxStartupPredicate { get; private set; }
        public Func<BaseDamage, bool> BaseDamagePredicate { get; private set; }
        public Func<Angle, bool> AnglePredicate { get; private set; }
        public Func<BaseKnockback, bool> BaseKnockbackPredicate { get; private set; }
        public Func<SetKnockback, bool> SetKnockbackPredicate { get; private set; }
        public Func<KnockbackGrowth, bool> KnockbackGrowthPredicate { get; private set; }
        public Func<Hitbox, bool> HitboxActiveOnFramePredicate { get; private set; }
        public Func<Move, bool> FirstActionableFramePredicate { get; private set; }
        public Func<LandingLag, bool> LandingLagPredicate { get; private set; }
        public Func<Autocancel, bool> AutocancelPredicate { get; private set; }
        public Func<Move, bool> NamePredicate { get; private set; }
        public Func<Character, bool> CharacterNamePredicate { get; private set; }

        public SearchPredicateFactory()
        {
            _nameSearchPredicateService = new NameSearchPredicateService();
            _characterNameSearchPredicateService = new CharacterNameSearchPredicateService();
            _hitboxActiveSearchPredicateService = new HitboxActiveSearchPredicateService();
            _firstActionableFrameSearchPredicateService = new FirstActionableFrameSearchPredicateService();
            _landingLagSearchPredicateService = new LandingLagSearchPredicateService();
            _baseMoveHitboxPredicateService = new SearchPredicateService();
            _autocancelSearchPredicateService = new AutoCancelSearchPredicateService();
            _hitboxActiveLengthSearchPredicateService = new HitboxActiveLengthSearchPredicateService();
        }

        public void CreateSearchPredicates(MoveSearchModel searchModel)
        {
            HitboxActiveLengthPredicate = CreateHitboxActiveLengthPredicate(searchModel);
            HitboxStartupPredicate = CreateHitboxStartupPredicate(searchModel);
            BaseDamagePredicate = CreateBaseDamagePredicate(searchModel);
            AnglePredicate = CreateAnglePredicate(searchModel);
            BaseKnockbackPredicate = CreateBaseKnockbackPredicate(searchModel);
            SetKnockbackPredicate = CreateSetKnockbackPredicate(searchModel);
            KnockbackGrowthPredicate = CreateKnockbackGrowthPredicate(searchModel);
            HitboxActiveOnFramePredicate = CreateHitboxActiveOnFramePredicate(searchModel);
            FirstActionableFramePredicate = CreateFirstActionableFramePredicate(searchModel);
            LandingLagPredicate = CreateLandingLagPredicate(searchModel);
            NamePredicate = CreateNamePredicate(searchModel);
            CharacterNamePredicate = CreateCharacterNamePredicate(searchModel);
            AutocancelPredicate = CreateAutocancelPredicate(searchModel);
        }

        private Func<Hitbox, bool> CreateHitboxActiveLengthPredicate(MoveSearchModel searchModel)
            => _hitboxActiveLengthSearchPredicateService.GetHitboxActiveLengthPredicate(searchModel.HitboxActiveLength);

        private Func<Autocancel, bool> CreateAutocancelPredicate(MoveSearchModel searchModel)
            => _autocancelSearchPredicateService.GetAutoCancelSearchPredicate(searchModel.AutoCancel);

        private Func<Hitbox, bool> CreateHitboxStartupPredicate(MoveSearchModel searchModel)
            => _baseMoveHitboxPredicateService.GetPredicate<Hitbox>(searchModel.HitboxStartupFrame);

        private Func<BaseDamage, bool> CreateBaseDamagePredicate(MoveSearchModel searchModel)
            => _baseMoveHitboxPredicateService.GetPredicate<BaseDamage>(searchModel.BaseDamage);

        private Func<Angle, bool> CreateAnglePredicate(MoveSearchModel searchModel)
            => _baseMoveHitboxPredicateService.GetPredicate<Angle>(searchModel.Angle);

        private Func<BaseKnockback, bool> CreateBaseKnockbackPredicate(MoveSearchModel searchModel)
            => _baseMoveHitboxPredicateService.GetPredicate<BaseKnockback>(searchModel.BaseKnockback);

        private Func<SetKnockback, bool> CreateSetKnockbackPredicate(MoveSearchModel searchModel)
           => _baseMoveHitboxPredicateService.GetPredicate<SetKnockback>(searchModel.SetKnockback);

        private Func<KnockbackGrowth, bool> CreateKnockbackGrowthPredicate(MoveSearchModel searchModel)
           => _baseMoveHitboxPredicateService.GetPredicate<KnockbackGrowth>(searchModel.KnockbackGrowth);

        private Func<Hitbox, bool> CreateHitboxActiveOnFramePredicate(MoveSearchModel searchModel)
            => _hitboxActiveSearchPredicateService.GetHitboxActivePredicate(searchModel.HitboxActiveOnFrame);

        private Func<Move, bool> CreateFirstActionableFramePredicate(MoveSearchModel searchModel)
           => _firstActionableFrameSearchPredicateService.GetFirstActionableFrameSearchPredicate(searchModel.FirstActionableFrame);

        private Func<LandingLag, bool> CreateLandingLagPredicate(MoveSearchModel searchModel)
            => _landingLagSearchPredicateService.GetLandingLagSearchPredicate(searchModel.LandingLag);

        private Func<Move, bool> CreateNamePredicate(MoveSearchModel searchModel)
            => _nameSearchPredicateService.GetNamePredicate(searchModel.Name);

        private Func<Character, bool> CreateCharacterNamePredicate(MoveSearchModel searchModel)
            => _characterNameSearchPredicateService.GetCharacterNamePredicate(searchModel.CharacterName);


    }
}

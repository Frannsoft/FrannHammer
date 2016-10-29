using System;
using System.Collections.Generic;
using FrannHammer.Core;
using FrannHammer.Models;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;

namespace FrannHammer.Services
{
    public interface IMetadataService
    {
        /// <summary>
        /// Join on Entity Id instead of MoveId.  If you want to join on MoveId use GetWithMoves.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        dynamic GetWithMovesOnEntity<TEntity, TDto>(int id, string fields = "")
            where TEntity : class, IMoveIdEntity
            where TDto : class;

        /// <summary>
        /// Joins with the moves table to get back all of type <typeparamref name="TEntity"/>
        /// with a matching MoveId.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        dynamic GetWithMoves<TEntity, TDto>(int id, string fields = "")
        where TEntity : class, IMoveIdEntity
            where TDto : class;

        /// <summary>
        /// Get all of type <typeparamref name="TEntity"/>.  Joins with moves table
        /// and brings back matching MoveId values.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="fields"></param>
        /// <returns></returns>
        IEnumerable<dynamic> GetAllWithMoves<TEntity, TDto>(string fields = "")
            where TEntity : class, IMoveIdEntity
            where TDto : class;

        /// <summary>
        /// Get all entities of type <typeparamref name="TPrimaryEntity"/> that have <typeparamref name="id"/>
        /// of the specified value of type <typeparamref name="TJoinEntity"/>.
        ///  </summary>
        /// <typeparam name="TJoinEntity"></typeparam>
        /// <typeparam name="TPrimaryEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        IEnumerable<dynamic> GetAllForOwnerId<TJoinEntity, TPrimaryEntity, TDto>(int id, string fields = "")
            where TJoinEntity : class, IMoveEntity
            where TPrimaryEntity : class, IMoveIdEntity
            where TDto : class;

        /// <summary>
        /// Get a single entity.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        dynamic Get<TEntity, TDto>(int id, string fields = "")
            where TEntity : class, IEntity
            where TDto : class;

        /// <summary>
        /// Get an entity where the expression is true.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="whereCondition"></param>
        /// <param name="fields"></param>
        /// <param name="isValidatable"></param>
        /// <returns></returns>
        dynamic Get<TEntity, TDto>(Expression<Func<TEntity, bool>> whereCondition, string fields = "", bool isValidatable = true)
            where TEntity : class, IEntity
            where TDto : class;

        /// <summary>
        /// Get fields of the specified entities where the expression is true.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="whereCondition"></param>
        /// <param name="fields"></param>
        /// <param name="isValidatable"></param>
        /// <returns></returns>
        IEnumerable<dynamic> GetAll<TEntity, TDto>(Expression<Func<TEntity, bool>> whereCondition, string fields = "",
            bool isValidatable = true)
            where TEntity : class, IEntity
            where TDto : class;

        /// <summary>
        /// Get fields of the specified entities where the expression is true.
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="searchModel"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        IEnumerable<dynamic> GetAll<TDto>(ComplexMoveSearchModel searchModel, string fields = "")
            where TDto : class;

        /// <summary>
        /// Get all entity data of a specific type.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        IEnumerable<dynamic> GetAll<TEntity, TDto>(string fields = "")
            where TEntity : class, IEntity
            where TDto : class;

        /// <summary>
        /// Update an existing entity.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        void Update<TEntity, TDto>(int id, TDto dto)
            where TEntity : class, IEntity
            where TDto : class;

        /// <summary>
        /// Add an entity.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        TDto Add<TEntity, TDto>(TDto dto)
            where TEntity : class, IEntity
            where TDto : class;

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="id"></param>
        void Delete<T>(int id)
            where T : class, IEntity;
    }

    public class MetadataService : BaseService, IMetadataService
    {
        protected IResultValidationService ResultValidationService { get; }

        public MetadataService(IApplicationDbContext db, IResultValidationService resultValidationService)
            : base(db)
        {
            Guard.VerifyObjectNotNull(resultValidationService, nameof(resultValidationService));
            ResultValidationService = resultValidationService;
        }

        public dynamic GetWithMovesOnEntity<TEntity, TDto>(int id, string fields = "")
            where TEntity : class, IMoveIdEntity
            where TDto : class
        {
            var dto = (from entity in Db.Set<TEntity>()
                       join joinEntity in Db.Moves
                           on entity.MoveId equals joinEntity.Id
                       where entity.Id == id
                       select entity).ProjectTo<TDto>()
                         .SingleOrDefault();

            ResultValidationService.ValidateSingleResult<TDto, TDto>(dto, id);
            return BuildContentResponse<TDto, TDto>(dto, fields);
        }

        public dynamic GetWithMoves<TEntity, TDto>(int id, string fields = "")
            where TEntity : class, IMoveIdEntity
            where TDto : class
        {
            var dto = (from entity in Db.Set<TEntity>()
                       join joinEntity in Db.Moves
                           on entity.MoveId equals joinEntity.Id
                       where entity.MoveId == id
                       select entity).ProjectTo<TDto>()
                         .SingleOrDefault();

            ResultValidationService.ValidateSingleResult<TDto, TDto>(dto, id);
            return BuildContentResponse<TDto, TDto>(dto, fields);
        }

        public IEnumerable<dynamic> GetAllWithMoves<TEntity, TDto>(string fields = "")
            where TEntity : class, IMoveIdEntity
            where TDto : class
        {
            var entities = (from entity in Db.Set<TEntity>()
                            join joinEntity in Db.Moves
                                on entity.MoveId equals joinEntity.Id
                            select entity).ProjectTo<TDto>().ToList();

            ResultValidationService.ValidateMultipleResult<TDto, TDto>(entities);
            return BuildContentResponseMultiple<TDto, TDto>(entities, fields);
        }

        public IEnumerable<dynamic> GetAllForOwnerId<TJoinEntity, TPrimaryEntity, TDto>(int id, string fields = "")
            where TJoinEntity : class, IMoveEntity
            where TPrimaryEntity : class, IMoveIdEntity
            where TDto : class
        {
            var entities = (from entity in Db.Set<TPrimaryEntity>()
                            join ret in Db.Set<TJoinEntity>()
                                on entity.MoveId equals ret.Id
                            where ret.OwnerId == id
                            select entity).ProjectTo<TDto>().ToList();

            ResultValidationService.ValidateMultipleResult<TDto, TDto>(entities, id);
            return BuildContentResponseMultiple<TDto, TDto>(entities, fields);
        }

        public dynamic Get<TEntity, TDto>(int id, string fields = "")
            where TEntity : class, IEntity
            where TDto : class
        {
            var entity = Db.Set<TEntity>().Find(id);

            ResultValidationService.ValidateSingleResult<TEntity, TDto>(entity, id);
            return BuildContentResponse<TEntity, TDto>(entity, fields);
        }

        public dynamic Get<TEntity, TDto>(Expression<Func<TEntity, bool>> where, string fields = "", bool isValidatable = true)
            where TEntity : class, IEntity
            where TDto : class
        {
            var entity = Db.Set<TEntity>().FirstOrDefault(where);

            if (isValidatable)
            { ResultValidationService.ValidateSingleResultFromExpression(entity, where); }

            return BuildContentResponse<TEntity, TDto>(entity, fields);
        }

        public IEnumerable<dynamic> GetAll<TEntity, TDto>(Expression<Func<TEntity, bool>> where, string fields = "", bool isValidatable = true)
            where TEntity : class, IEntity
            where TDto : class
        {
            var entities = Db.Set<TEntity>()
                             .Where(where)
                             .ProjectTo<TDto>()
                             .ToList();

            if (isValidatable)
            { ResultValidationService.ValidateMultipleResultFromExpression(entities, where); }

            return BuildContentResponseMultiple<TDto, TDto>(entities, fields);
        }

        public IEnumerable<dynamic> GetAll<TDto>(ComplexMoveSearchModel searchModel, string fields = "")
            where TDto : class
        {
            var searchPredicateFactory = new SearchPredicateFactory();
            searchPredicateFactory.CreateSearchPredicates(searchModel);

            //character name ids need to be separate since all other are move ids.
            var characterNames = GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.CharacterNamePredicate).Select(c => c.Id);

            var names = GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.NamePredicate)?.Select(m => m.Id);
            var hitboxActiveLengths = GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.HitboxActiveLengthPredicate)?.Select(h => h.MoveId);
            var hitboxStartups = GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.HitboxStartupPredicate)?.Select(h => h.MoveId);
            var hitboxActives = GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.HitboxActiveOnFramePredicate)?.Select(h => h.MoveId);
            var baseDamages = GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.BaseDamagePredicate)?.Select(b => b.MoveId);
            var angles = GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.AnglePredicate)?.Select(a => a.MoveId);
            var baseKnockbacks = GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.BaseKnockbackPredicate)?.Select(b => b.MoveId);
            var setKnockbacks = GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.SetKnockbackPredicate)?.Select(s => s.MoveId);
            var knockbackGrowths = GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.KnockbackGrowthPredicate)?.Select(k => k.MoveId);
            var firstActionableFrames = GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.FirstActionableFramePredicate)?.Select(f => f.Id);
            var landingLags = GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.LandingLagPredicate)?.Select(l => l.MoveId);
            var autocancels = GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.AutocancelPredicate)?.Select(a => a.MoveId);

            var combinedTotalMoveIds = new List<int>()
                .SafeConcat(names)
                .SafeConcat(hitboxActiveLengths)
                .SafeConcat(hitboxStartups)
                .SafeConcat(hitboxActives)
                .SafeConcat(baseDamages)
                .SafeConcat(angles)
                .SafeConcat(baseKnockbacks)
                .SafeConcat(setKnockbacks)
                .SafeConcat(knockbackGrowths)
                .SafeConcat(firstActionableFrames)
                .SafeConcat(landingLags)
                .SafeConcat(autocancels).Distinct().ToList();

            var foundMoves = Db.Moves.Where(m => combinedTotalMoveIds.Contains(m.Id) && characterNames.Contains(m.OwnerId))
                            .ProjectTo<TDto>().ToList();

            return BuildContentResponseMultiple<TDto, TDto>(foundMoves, fields);
        }

        private IList<T> GetEntitiesThatMeetSearchCriteria<T>(Func<T, bool> searchPredicate)
            where T : class
        {
            if(searchPredicate == null)
            { return null; }

            return Db.Set<T>()
                .Where(searchPredicate).ToList();
        }

        /// <summary>
        /// Get all entity data of a specific type.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public virtual IEnumerable<dynamic> GetAll<TEntity, TDto>(string fields = "")
            where TEntity : class, IEntity
            where TDto : class
        {
            var entities = Db.Set<TEntity>()
                              .ProjectTo<TDto>()
                              .ToList();

            ResultValidationService.ValidateMultipleResult<TDto, TDto>(entities);
            return BuildContentResponseMultiple<TDto, TDto>(entities, fields);
        }

        /// <summary>
        /// Update an existing entity.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        public void Update<TEntity, TDto>(int id, TDto dto)
            where TEntity : class, IEntity
            where TDto : class
        {
            Guard.VerifyObjectNotNull(dto, nameof(dto));
            UpdateEntity<TEntity, TDto>(id, dto);
        }

        /// <summary>
        /// Add an entity.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public TDto Add<TEntity, TDto>(TDto dto)
            where TEntity : class, IEntity
            where TDto : class
        {
            Guard.VerifyObjectNotNull(dto, nameof(dto));
            return AddEntity<TEntity, TDto>(dto);
        }

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="id"></param>
        public void Delete<T>(int id)
            where T : class, IEntity => DeleteEntity<T>(id);
    }
}

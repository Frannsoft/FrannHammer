using System;
using System.Collections.Generic;
using FrannHammer.Core;
using FrannHammer.Models;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FrannHammer.Models.DTOs;
using FrannHammer.Services.MoveSearch;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace FrannHammer.Services
{
    public interface IMetadataService
    {
        /// <summary>
        /// Gets the standard model of an object without support for mapping or fields parameter customization.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById<T>(int id)
            where T : class, IEntity;

        /// <summary>
        /// Gets the standard model of objects of a db type without support for mapping or fields parameter customization.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllOfType<T>()
            where T : class, IEntity;

        /// <summary>
        /// Join on Entity Id instead of MoveId.  If you want to join on MoveId use GetWithMoves.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        IDictionary<string, object> GetWithMovesOnEntity<TEntity, TDto>(int id, string fields = "")
            where TEntity : class, IMoveIdEntity
            where TDto : class;

        /// <summary>
        /// Joins with the moves table to get back first of type <typeparamref name="TEntity"/>
        /// with a matching MoveId.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        IDictionary<string, object> GetWithMoves<TEntity, TDto>(int id, string fields = "")
        where TEntity : class, IMoveIdEntity
            where TDto : class;

        /// <summary>
        /// Joins with the moves table to get back all of type <typeparamref name="TEntity"/>
        /// with a matching MoveId.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="ids"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        IEnumerable<IDictionary<string, object>> GetMultipleWithMoves<TEntity, TDto>(IEnumerable<int> ids, string fields = "")
            where TEntity : class, IMoveIdEntity
            where TDto : class;

        /// <summary>
        /// Gets all the attributes of a <see cref="Move"/> using the strongly typed model tables 
        /// instead of the raw <see cref="Move"/> data from the <see cref="Move"/> table.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        IEnumerable<DetailedMoveDto> GetDetailsForMovesOfCharacter(int id, string fields = "");

        /// <summary>
        /// Get all of type <typeparamref name="TEntity"/>.  Joins with moves table
        /// and brings back matching MoveId values.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="fields"></param>
        /// <returns></returns>
        IEnumerable<IDictionary<string, object>> GetAllWithMoves<TEntity, TDto>(string fields = "")
            where TEntity : class, IMoveIdEntity
            where TDto : class;

        /// <summary>
        /// Get all entities of type <typeparamref name="TPrimaryEntity"/> that have <typeparamref name="id"/>.
        ///  </summary>
        /// <typeparam name="TPrimaryEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        IEnumerable<IDictionary<string, object>> GetAllMoveDataForOwnerId<TPrimaryEntity, TDto>(int id, string fields = "")
            where TPrimaryEntity : class, IMoveIdEntity
            where TDto : class;

        /// <summary>
        /// Get a single entity.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        IDictionary<string, object> Get<TEntity, TDto>(int id, string fields = "")
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
        IDictionary<string, object> Get<TEntity, TDto>(Expression<Func<TEntity, bool>> whereCondition, string fields = "", bool isValidatable = true)
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
        IEnumerable<IDictionary<string, object>> GetAll<TEntity, TDto>(Expression<Func<TEntity, bool>> whereCondition, string fields = "",
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
        IEnumerable<IDictionary<string, object>> GetAll<TDto>(MoveSearchModel searchModel, IConnectionMultiplexer redisConnectionMultiplexer, string fields = "")
            where TDto : MoveSearchDto;

        /// <summary>
        /// Get all entity data of a specific type.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        IEnumerable<IDictionary<string, object>> GetAll<TEntity, TDto>(string fields = "")
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
        private readonly MoveSearchModelRedisService _moveSearchModelRedisService;

        public MetadataService(IApplicationDbContext db, IResultValidationService resultValidationService)
            : base(db)
        {
            Guard.VerifyObjectNotNull(resultValidationService, nameof(resultValidationService));
            ResultValidationService = resultValidationService;

            _moveSearchModelRedisService = new MoveSearchModelRedisService();
        }

        public IDictionary<string, object> GetWithMovesOnEntity<TEntity, TDto>(int id, string fields = "")
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

        public IEnumerable<DetailedMoveDto> GetDetailsForMovesOfCharacter(int id, string fields = "")
        {
            // get all moveIds for moves for character specified by id
            var moveData = GetAll<Move, MoveDto>(m => m.OwnerId == id, $"{nameof(Move.Id)},{nameof(Move.Name)},{nameof(Move.FirstActionableFrame)}").ToList();

            //all move attribute tables have these same columns.  We don't need the metadata (ownerid, name, etc.) since 
            //we have that from the above call.
            const string moveDetailFields = "id,hitbox1,hitbox2,hitbox3,hitbox4,hitbox5,hitbox6,notes";

            IList<DetailedMoveDto> detailedMoves = new List<DetailedMoveDto>();

            if (moveData.Count != 0)
            {
                foreach (var data in moveData)
                {
                    int moveId = (int)data["Id"];
                    string moveName = (string)data["Name"];

                    //search each move attribute table for values
                    var angles = GetWithMoves<Angle, AngleDto>(moveId, moveDetailFields);
                    var hitboxes = GetWithMoves<Hitbox, HitboxDto>(moveId, moveDetailFields);
                    var baseDamages = GetWithMoves<BaseDamage, BaseDamageDto>(moveId, moveDetailFields);
                    var baseKnockbacks = GetWithMoves<BaseKnockback, BaseKnockbackDto>(moveId, moveDetailFields);
                    var setKnockbacks = GetWithMoves<SetKnockback, SetKnockbackDto>(moveId, moveDetailFields);
                    var autoCancels = GetWithMoves<Autocancel, AutocancelDto>(moveId);
                    var landingLags = GetWithMoves<LandingLag, LandingLagDto>(moveId);
                    var firstActionableFrame = GetWithMoves<FirstActionableFrame, FirstActionableFrameDto>(moveId, "Frame");

                    //aggregate into Dto 
                    var detailedMoveDto = new DetailedMoveDto
                    {
                        MoveId = moveId,
                        MoveName = moveName,
                        Angle = angles,
                        Hitbox = hitboxes,
                        BaseDamage = baseDamages,
                        BaseKnockback = baseKnockbacks,
                        SetKnockback = setKnockbacks,
                        Autocancel = autoCancels,
                        LandingLag = landingLags,
                        FirstActionableFrame = firstActionableFrame
                    };

                    detailedMoves.Add(detailedMoveDto);
                }
            }
            else
            {
                return new List<DetailedMoveDto>();
            }

            return detailedMoves;
        }

        public IDictionary<string, object> GetWithMoves<TEntity, TDto>(int id, string fields = "")
            where TEntity : class, IMoveIdEntity
            where TDto : class
        {
            var dto = (from entity in Db.Set<TEntity>()
                       join joinEntity in Db.Moves
                           on entity.MoveId equals joinEntity.Id
                       where entity.MoveId == id
                       select entity).ProjectTo<TDto>()
                         .SingleOrDefault();

            //ResultValidationService.ValidateSingleResult<TDto, TDto>(dto, id);
            return BuildContentResponse<TDto, TDto>(dto, fields);
            //return response ?? $"No data of type {typeof(TEntity).Name} found with Move id {id}";
        }

        public IEnumerable<IDictionary<string, object>> GetMultipleWithMoves<TEntity, TDto>(IEnumerable<int> ids, string fields = "")
            where TEntity : class, IMoveIdEntity
            where TDto : class
        {
            var dtos = (from entity in Db.Set<TEntity>()
                        join joinEntity in Db.Moves
                        on entity.MoveId equals joinEntity.Id
                        where ids.Contains(entity.MoveId)
                        select entity).ProjectTo<TDto>().ToList();

            //ResultValidationService.ValidateSingleResult<TDto, TDto>(dto, id);
            return BuildContentResponseMultiple<TDto, TDto>(dtos, fields);
            //return response ?? $"No data of type {typeof(TEntity).Name} found with Move id {id}";
        }

        public IEnumerable<IDictionary<string, object>> GetAllWithMoves<TEntity, TDto>(string fields = "")
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

        public IEnumerable<IDictionary<string, object>> GetAllMoveDataForOwnerId<TPrimaryEntity, TDto>(int id, string fields = "")
            where TPrimaryEntity : class, IMoveIdEntity
            where TDto : class
        {
            var entities = (from entity in Db.Set<TPrimaryEntity>()
                            join ret in Db.Set<Move>()
                                on entity.MoveId equals ret.Id
                            where ret.OwnerId == id
                            select entity).ProjectTo<TDto>().ToList();

            ResultValidationService.ValidateMultipleResult<TDto, TDto>(entities, id);
            return BuildContentResponseMultiple<TDto, TDto>(entities, fields);
        }

        public IDictionary<string, object> Get<TEntity, TDto>(int id, string fields = "")
            where TEntity : class, IEntity
            where TDto : class
        {
            var entity = Db.Set<TEntity>().Find(id);

            ResultValidationService.ValidateSingleResult<TEntity, TDto>(entity, id);
            return BuildContentResponse<TEntity, TDto>(entity, fields);
        }

        public IDictionary<string, object> Get<TEntity, TDto>(Expression<Func<TEntity, bool>> where, string fields = "", bool isValidatable = true)
            where TEntity : class, IEntity
            where TDto : class
        {
            var entity = Db.Set<TEntity>().FirstOrDefault(where);

            if (isValidatable)
            { ResultValidationService.ValidateSingleResultFromExpression(entity, where); }

            return BuildContentResponse<TEntity, TDto>(entity, fields);
        }

        public IEnumerable<IDictionary<string, object>> GetAll<TEntity, TDto>(Expression<Func<TEntity, bool>> where, string fields = "", bool isValidatable = true)
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

        public IEnumerable<IDictionary<string, object>> GetAll<TDto>(MoveSearchModel searchModel, IConnectionMultiplexer redisConnectionMultiplexer,
            string fields = "")
            where TDto : MoveSearchDto
        {
            IEnumerable<IDictionary<string, object>> results = default(IEnumerable<IDictionary<string, object>>);
            IDatabase redisDatabase = default(IDatabase);
            string redisValueForKey = string.Empty;
            string redisKey = string.Empty;

            if (redisConnectionMultiplexer != null && redisConnectionMultiplexer.IsConnected)
            {
                //check if exists in redis cache if have access to redis
                redisKey = _moveSearchModelRedisService.MoveSearchModelToRedisKey(searchModel, fields);

                redisDatabase = redisConnectionMultiplexer.GetDatabase();
                redisValueForKey = redisDatabase.StringGet(redisKey);
            }

            //if search already exists in cache just return the results.  Api data is almost all static.
            if (!string.IsNullOrEmpty(redisValueForKey) && redisValueForKey != RedisValue.Null)
            {
                results = JsonConvert.DeserializeObject<IEnumerable<IDictionary<string, object>>>(redisValueForKey);
            }
            else
            {
                var searchPredicateFactory = new SearchPredicateFactory();
                searchPredicateFactory.CreateSearchPredicates(searchModel);

                //character name ids need to be separate since all other are move ids.
                var characterNames =
                    GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.CharacterNamePredicate).Select(c => c.Id);

                var names = GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.NamePredicate)?.Select(m => m.Id);
                var hitboxActiveLengths =
                    GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.HitboxActiveLengthPredicate)?
                        .Select(h => h.MoveId);
                var hitboxStartups =
                    GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.HitboxStartupPredicate)?
                        .Select(h => h.MoveId);
                var hitboxActives =
                    GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.HitboxActiveOnFramePredicate)?
                        .Select(h => h.MoveId);
                var baseDamages =
                    GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.BaseDamagePredicate)?.Select(b => b.MoveId);
                var angles =
                    GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.AnglePredicate)?.Select(a => a.MoveId);
                var baseKnockbacks =
                    GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.BaseKnockbackPredicate)?
                        .Select(b => b.MoveId);
                var setKnockbacks =
                    GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.SetKnockbackPredicate)?
                        .Select(s => s.MoveId);
                var knockbackGrowths =
                    GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.KnockbackGrowthPredicate)?
                        .Select(k => k.MoveId);
                var firstActionableFrames =
                    GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.FirstActionableFramePredicate)?
                        .Select(f => f.Id);
                var landingLags =
                    GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.LandingLagPredicate)?.Select(l => l.MoveId);
                var autocancels =
                    GetEntitiesThatMeetSearchCriteria(searchPredicateFactory.AutocancelPredicate)?.Select(a => a.MoveId);

                var combinedTotalMoveIds = new MoveSearchResultCollection<int>()
                    .SafeFilter(names)
                    .SafeFilter(hitboxActiveLengths)
                    .SafeFilter(hitboxStartups)
                    .SafeFilter(hitboxActives)
                    .SafeFilter(baseDamages)
                    .SafeFilter(angles)
                    .SafeFilter(baseKnockbacks)
                    .SafeFilter(setKnockbacks)
                    .SafeFilter(knockbackGrowths)
                    .SafeFilter(firstActionableFrames)
                    .SafeFilter(landingLags)
                    .SafeFilter(autocancels);

                IList<TDto> foundMoves;

                if (combinedTotalMoveIds.Items.Count > 0)
                {
                    foundMoves = Db.Moves.Where(m => combinedTotalMoveIds.Items.Contains(m.Id) && characterNames.Contains(m.OwnerId))
                        .ProjectTo<TDto>().ToList();

                    ApplyCharacterDetailsToMove(foundMoves);
                    results = BuildContentResponseMultiple<TDto, TDto>(foundMoves, fields);
                    CacheResults(redisDatabase, redisKey, results);
                }
                else if (!string.IsNullOrEmpty(searchModel.CharacterName) &&
                        combinedTotalMoveIds.Items.Count == 0)
                {
                    foundMoves = Db.Moves.Where(m => characterNames.Contains(m.OwnerId))
                        .ProjectTo<TDto>().ToList();

                    ApplyCharacterDetailsToMove(foundMoves);
                    results = BuildContentResponseMultiple<TDto, TDto>(foundMoves, fields);
                    CacheResults(redisDatabase, redisKey, results);
                }
            }
            //return empty list if no results found
            return results ?? new List<Dictionary<string, object>>();
        }

        private void CacheResults(IDatabase redisDatabase, string redisKey,
            IEnumerable<dynamic> results)
        {
            if (redisDatabase != null)
            {
                // add key and results so they exist in the future
                string resultsJson = JsonConvert.SerializeObject(results);
                redisDatabase.StringSet(redisKey, resultsJson);
            }
        }

        private void ApplyCharacterDetailsToMove<T>(IEnumerable<T> moves)
            where T : MoveSearchDto
        {
            foreach (var move in moves)
            {
                move.Owner = Mapper.Map<CharacterDto>(Db.Characters.Find(move.OwnerId));
            }
        }

        private IList<T> GetEntitiesThatMeetSearchCriteria<T>(Func<T, bool> searchPredicate)
            where T : class
        {
            if (searchPredicate == null)
            { return null; }

            return Db.Set<T>()
                .Where(searchPredicate).ToList();
        }

        /// <summary>
        /// Get all entity data of a specific type.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public virtual IEnumerable<IDictionary<string, object>> GetAll<TEntity, TDto>(string fields = "")
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

        public T GetById<T>(int id)
            where T : class, IEntity
        {
            return Db.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAllOfType<T>()
            where T : class, IEntity
        {
            return Db.Set<T>();
        }
    }
}

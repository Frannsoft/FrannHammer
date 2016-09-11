using System;
using System.Collections.Generic;
using FrannHammer.Core;
using FrannHammer.Models;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using FrannHammer.Services.Exceptions;

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

        ///// <summary>
        ///// Forms an return value from a previously found entity.
        ///// </summary>
        ///// <typeparam name="TEntity"></typeparam>
        ///// <typeparam name="TDto"></typeparam>
        ///// <param name="entity"></param>
        ///// <param name="fields"></param>
        ///// <returns></returns>
        //dynamic GetFromEntity<TEntity, TDto>(TEntity entity, string fields = "")
        //    where TEntity : class, IMoveIdEntity
        //    where TDto : class;

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
        /// <returns></returns>
        dynamic Get<TEntity, TDto>(Expression<Func<TEntity, bool>> whereCondition, string fields = "")
            where TEntity : class, IEntity
            where TDto : class;

        /// <summary>
        /// Get fields of the specified entities where the expression is true.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="whereCondition"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        IEnumerable<dynamic> GetAll<TEntity, TDto>(Expression<Func<TEntity, bool>> whereCondition, string fields = "")
            where TEntity : class, IEntity
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
        public MetadataService(IApplicationDbContext db)
            : base(db)
        { }

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

            ValidateSingleResult<TDto, TDto>(dto, id);
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

            ValidateSingleResult<TDto, TDto>(dto, id);
            return BuildContentResponse<TDto, TDto>(dto, fields);
        }

        //public dynamic GetFromEntity<TEntity, TDto>(TEntity entity, string fields = "") 
        //    where TEntity : class, IMoveIdEntity 
        //    where TDto : class
        //{
        //    return BuildContentResponse<TEntity, TDto>(entity, fields);
        //}

        public IEnumerable<dynamic> GetAllWithMoves<TEntity, TDto>(string fields = "") 
            where TEntity : class, IMoveIdEntity 
            where TDto : class
        {
            var entities = (from entity in Db.Set<TEntity>()
                       join joinEntity in Db.Moves
                           on entity.MoveId equals joinEntity.Id
                       select entity).ProjectTo<TDto>().ToList();

            ValidateMultipleResult<TDto, TDto>(entities);
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

            ValidateMultipleResult<TDto, TDto>(entities, id);
            return BuildContentResponseMultiple<TDto, TDto>(entities, fields);
        }

        public dynamic Get<TEntity, TDto>(int id, string fields = "")
            where TEntity : class, IEntity
            where TDto : class
        {
            var entity = Db.Set<TEntity>().Find(id);

            ValidateSingleResult<TEntity, TDto>(entity, id);
            return BuildContentResponse<TEntity, TDto>(entity, fields);
        }

        public dynamic Get<TEntity, TDto>(Expression<Func<TEntity, bool>> where, string fields = "")
            where TEntity : class, IEntity
            where TDto : class
        {
            var entity = Db.Set<TEntity>().First(where);

            ValidateSingleResult<TEntity, TDto>(entity);
            return BuildContentResponse<TEntity, TDto>(entity, fields);
        }

        public IEnumerable<dynamic> GetAll<TEntity, TDto>(Expression<Func<TEntity, bool>> where, string fields = "")
            where TEntity : class, IEntity
            where TDto : class
        {
            var entities = Db.Set<TEntity>()
                             .Where(where)
                             .ProjectTo<TDto>()
                             .ToList();

            ValidateMultipleResult<TDto, TDto>(entities);
            return BuildContentResponseMultiple<TDto, TDto>(entities, fields);
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

            ValidateMultipleResult<TDto, TDto>(entities);
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

        private void ValidateSingleResult<TDto, TPrimaryModelType>(TDto entity, int id = -1)
        {
            if (entity == null)
            {
                string baseMessage = $"Unable to find any entities of type '{typeof(TPrimaryModelType).Name}'";
                string extraMessage = id > -1 ? $"with id = {id}" : string.Empty;

                throw new EntityNotFoundException(baseMessage + extraMessage);
            }
        }

        private void ValidateMultipleResult<TEntity, TDto>(IList<TEntity> entities, int id = -1)
        {
            if (entities == null || entities.Count == 0)
            {
                string baseMessage = $"Unable to find any entities of type '{typeof(TDto).Name}'";
                string extraMessage = id > -1 ? $"with id = {id}" : string.Empty;

                throw new EntityNotFoundException(baseMessage + extraMessage);
            }
        }
    }
}

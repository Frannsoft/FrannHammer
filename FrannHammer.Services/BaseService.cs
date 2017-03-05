using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using FrannHammer.Core;
using FrannHammer.Models;
using FrannHammer.Models.DTOs;
using FrannHammer.Services.Exceptions;

namespace FrannHammer.Services
{
    public abstract class BaseService
    {
        protected DtoBuilder DtoBuilder { get; }
        protected IApplicationDbContext Db { get; }

        protected BaseService(IApplicationDbContext db)
        {
            Guard.VerifyObjectNotNull(db, nameof(db));
            Db = db;
            DtoBuilder = new DtoBuilder();
        }

        protected void UpdateEntity<TEntity, TDto>(int id, TDto dto)
            where TEntity : class, IEntity
            where TDto : class
        {
            var entity = Db.Set<TEntity>().Find(id);

            if (entity == null)
            { throw new EntityNotFoundException($"Unable to find entity of {typeof(TEntity).Name} with id = {id}"); }

            entity = Mapper.Map(dto, entity);
            entity.LastModified = DateTime.Now;
            Db.Entry(entity).State = EntityState.Modified;

            Db.SaveChanges();
        }

        protected TDto AddEntity<TEntity, TDto>(TDto dto)
            where TEntity : class, IEntity
            where TDto : class
        {
            var entity = Mapper.Map<TDto, TEntity>(dto);
            entity.LastModified = DateTime.Now;

            Db.Set<TEntity>().Add(entity);
            Db.SaveChanges();

            var newDto = Mapper.Map<TEntity, TDto>(entity);
            return newDto;
        }

        protected void DeleteEntity<T>(int id)
            where T : class, IEntity
        {
            var entity = Db.Set<T>().Find(id);

            if (entity == null)
            { throw new EntityNotFoundException($"Unable to find entity of {typeof(T).Name} with id = {id}"); }

            Db.Set<T>().Remove(entity);
            Db.SaveChanges();
        }

        protected bool EntityExists<T>(int id)
            where T : class, IEntity => Db.Set<T>().Count(e => e.Id == id) > 0;

        /// <summary>
        /// Returns a content based response consisting of the user requested fields or the default
        /// response model object.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="entity"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        protected IDictionary<string, object> BuildContentResponse<TEntity, TDto>(TEntity entity, string fields)
            where TEntity : class
        {
            //if (entity == null)
            //{ throw new EntityNotFoundException($"Unable to find any entities of type '{typeof(TEntity).Name}'"); }

            return entity != null ? DtoBuilder.Build<TEntity, TDto>(entity, fields) : default(IDictionary<string, object>);
        }

        protected IEnumerable<IDictionary<string, object>> BuildContentResponseMultiple<TEntity, TDto>(IList<TEntity> entities,
            string fields)
            where TEntity : class
            where TDto : class
        {
            Guard.VerifyObjectNotNull(entities, nameof(entities));

            return entities.Select(entity => DtoBuilder.Build<TEntity, TDto>(entity, fields));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using FrannHammer.Core;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public abstract class BaseService
    {
        protected readonly DtoBuilder DtoBuilder;
        protected readonly IApplicationDbContext Db;

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
            { throw new NullReferenceException($"Unable to find entity of {typeof(TEntity).Name} with id = {id}"); }

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
            { throw new NullReferenceException($"Unable to find entity of {typeof(T).Name} with id = {id}"); }

            Db.Set<T>().Remove(entity);
            Db.SaveChanges();
        }

        protected bool EntityExists<T>(int id)
            where T : class, IEntity
        {
            return Db.Set<T>().Count(e => e.Id == id) > 0;
        }

        /// <summary>
        /// Returns a content based response that is either a custom <see cref="ExpandoObject"/> or an
        /// existing DTO depending on the passed in fields.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="entity"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        protected dynamic BuildContentResponse<TEntity, TDto>(TEntity entity, string fields)
            where TEntity : class
        {
            if (entity == null)
            {
                return null;
            }

            return new DtoBuilder().Build(entity, fields);

            //return !string.IsNullOrEmpty(fields) ?
            //    new DtoBuilder().Build(entity, fields) :
            //    Mapper.Map<TEntity, TDto>(entity);
        }

        protected IEnumerable<dynamic> BuildContentResponseMultiple<TEntity, TDto>(IQueryable<TEntity> entities,
            string fields)
            where TEntity : class
            where TDto : class
        {
            if (entities == null)
            {
                return null;
            }

            var entitiesList = entities.ToList(); //note: this evaluates the result set fully!

            var builder = new DtoBuilder();
            return from entity in entitiesList
                   select builder.Build(entity, fields);

            //if (!string.IsNullOrEmpty(fields))
            //{
            //    var builder = new DtoBuilder();
            //    return from entity in entitiesList
            //           select builder.Build(entity, fields);
            //}
            //else
            //{
            //    return (from entity in entitiesList
            //           select Mapper.Map<TEntity, TDto>(entity)).ToList();
            //}
        }
    }
}

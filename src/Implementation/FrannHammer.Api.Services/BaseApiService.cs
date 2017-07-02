using System;
using System.Collections.Generic;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;

namespace FrannHammer.Api.Services
{
    public abstract class BaseApiService<T> : ICrudService<T>
        where T : IModel
    {
        protected IRepository<T> Repository { get; }

        protected BaseApiService(IRepository<T> repository)
        {
            Guard.VerifyObjectNotNull(repository, nameof(repository));
            Repository = repository;
        }

        public T GetSingleByInstanceId(string id)
        {
            return GetSingleWhere(m => m.InstanceId == id);
        }

        public IEnumerable<T> GetAllWhereName(string name)
        {
            return GetAllWhere(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<T> GetAll()
        {
            return Repository.GetAll();
        }

        public void Add(T model)
        {
            Guard.VerifyObjectNotNull(model, nameof(model));
            Repository.Add(model);
        }

        public void AddMany(IEnumerable<T> moves)
        {
            Guard.VerifyObjectNotNull(moves, nameof(moves));
            Repository.AddMany(moves);
        }

        public T GetSingleWhere(Func<T, bool> @where)
        {
            var move = Repository.GetSingleWhere(where);
            return move;
        }

        public IEnumerable<T> GetAllWhere(Func<T, bool> @where)
        {
            var moves = Repository.GetAllWhere(where);
            return moves;
        }

        public IEnumerable<T> GetAllWhere(IDictionary<string, object> queryParameters)
        {
            var moves = Repository.GetAllWhere(queryParameters);
            return moves;
        }
    }
}

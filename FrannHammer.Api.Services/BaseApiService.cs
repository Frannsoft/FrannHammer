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

        public T GetSingleById(string id, string fields = "")
        {
            return GetSingleWhere(m => m.Id == id, fields);
        }

        public IEnumerable<T> GetAllWhereName(string name, string fields = "")
        {
            return GetAllWhere(m => m.Name == name, fields);
        }

        public IEnumerable<T> GetAll(string fields = "")
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

        public T GetSingleWhere(Func<T, bool> @where, string fields = "")
        {
            var move = Repository.GetSingleWhere(where);
            return move;
        }

        public IEnumerable<T> GetAllWhere(Func<T, bool> @where, string fields = "")
        {
            var moves = Repository.GetAllWhere(where);
            return moves;
        }
    }
}

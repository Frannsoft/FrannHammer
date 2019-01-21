using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FrannHammer.Api.Services
{
    public abstract class BaseApiService<T> : ICrudService<T>
        where T : IModel
    {
        protected IRepository<T> Repository { get; }

        private readonly Games _game;

        protected BaseApiService(IRepository<T> repository, string game)
        {
            Guard.VerifyObjectNotNull(repository, nameof(repository));
            Repository = repository;
            string adjustedCasingGame = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(game);
            _game = (Games)Enum.Parse(typeof(Games), adjustedCasingGame);
        }

        protected Func<T, bool> WhereGameIs() => g => g.Game == _game;

        public T GetSingleByInstanceId(string id)
        {
            return GetSingleWhere(m => m.InstanceId == id);
        }

        public IEnumerable<T> GetAllWhereName(string name)
        {
            return GetAllWhere(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && m.Game == _game);
        }

        public IEnumerable<T> GetAll()
        {
            return Repository.GetAll().Where(item => item.Game == _game);
        }

        public void Add(T model)
        {
            Guard.VerifyObjectNotNull(model, nameof(model));
            Repository.Add(model);
        }

        public void AddMany(IEnumerable<T> items)
        {
            Guard.VerifyObjectNotNull(items, nameof(items));
            Repository.AddMany(items);
        }

        public T GetSingleWhere(Func<T, bool> @where)
        {
            var move = Repository.GetSingleWhere(where);
            return move;
        }

        public IEnumerable<T> GetAllWhere(Func<T, bool> @where)
        {
            var moves = Repository.GetAllWhere(where).Where(i => i.Game == _game);

            return moves;
        }

        public IEnumerable<T> GetAllWhere(IDictionary<string, object> queryParameters)
        {
            var moves = Repository.GetAllWhere(queryParameters);
            return moves;
        }
    }
}

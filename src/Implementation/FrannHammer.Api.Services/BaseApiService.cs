using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.Api.Services
{
    public abstract class BaseApiService<T> : ICrudService<T>
        where T : IModel
    {
        private Games _game;

        protected IRepository<T> Repository { get; }

        protected BaseApiService(IRepository<T> repository, IGameParameterParserService gameParameterParserService)
        {
            Guard.VerifyObjectNotNull(repository, nameof(repository));
            Repository = repository;
            _game = gameParameterParserService.ParseGame();
        }

        protected Func<T, bool> WhereGameIs() => g => g.Game == _game;

        public T GetSingleByInstanceId(string id)
        {
            var foundItem = GetSingleWhere(m => m.InstanceId == id && m.Game == _game);
            return foundItem;
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
            var item = Repository.GetSingleWhere(where);

            if (item == null)
            {
                //if the item wasn't found, maybe the user is looking for an Ultimate
                //resource, but didn't specify Ultimate on the request.
                //let's make their life a bit easier and check Ultimate for them.
                //If nothing is still found we can throw a not found exception
                //knowing the desired couldn't be found in either game.
                if (_game == Games.Smash4)
                {
                    _game = Games.Ultimate;
                    item = Repository.GetSingleWhere(where);
                }
                if (item == null)
                {
                    throw new ResourceNotFoundException($"Resource of type '{typeof(T).Name}' not found.");
                }
            }

            return item;
        }

        public IEnumerable<T> GetAllWhere(Func<T, bool> @where)
        {
            var items = Repository.GetAllWhere(where).Where(i => i.Game == _game);

            if (!items.Any())
            {
                //if the item wasn't found, maybe the user is looking for an Ultimate
                //resource, but didn't specify Ultimate on the request.
                //let's make their life a bit easier and check Ultimate for them.
                //If nothing is still found we can throw a not found exception
                //knowing the desired couldn't be found in either game.
                if (_game == Games.Smash4)
                {
                    _game = Games.Ultimate;
                    items = Repository.GetAllWhere(where).Where(i => i.Game == _game);
                }
            }

            return items;
        }
    }
}

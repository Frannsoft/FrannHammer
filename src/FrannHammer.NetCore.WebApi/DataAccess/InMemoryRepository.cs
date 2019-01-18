using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace FrannHammer.NetCore.WebApi.DataAccess
{
    public class InMemoryRepository<T> : IRepository<T>
        where T : class, IModel
    {
        private readonly List<T> _data;

        public InMemoryRepository(List<T> data)
        {
            Guard.VerifyObjectNotNull(data, nameof(data));
            _data = data;
        }

        public T Add(T model)
        {
            Guard.VerifyObjectNotNull(model, nameof(model));
            _data.Add(model);
            return model;
        }

        public void AddMany(IEnumerable<T> models)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return _data;
        }

        public IEnumerable<T> GetAllWhere(Func<T, bool> where)
        {
            return _data.Where(where);
        }

        public IEnumerable<T> GetAllWhere(IDictionary<string, object> queryParameters)
        {
            throw new NotImplementedException();
        }

        public T GetSingleWhere(Func<T, bool> where)
        {
            return _data.SingleOrDefault(where);
        }

        public void Update(T model)
        {
            var match = _data.FirstOrDefault(d => d.InstanceId.Equals(model.InstanceId, StringComparison.OrdinalIgnoreCase));

            if (match != null)
            {
                _data.Remove(model);
                _data.Add(match);
            }
            else
            {
                throw new ResourceNotFoundException($"Resource of type '{typeof(T).Name}' with id '{model.InstanceId}' not found.");
            }
        }
    }
}

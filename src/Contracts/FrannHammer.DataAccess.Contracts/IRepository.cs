using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.DataAccess.Contracts
{
    public interface IRepository<T>
        where T : IModel
    {
        T GetSingleWhere(Func<T, bool> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllWhere(Func<T, bool> where);
        IEnumerable<T> GetAllWhere(IDictionary<string, object> queryParameters);
        void Update(T model);
        T Add(T model);
        void AddMany(IEnumerable<T> models);
        void Delete(string id);
    }
}

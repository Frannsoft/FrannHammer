using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.DataAccess.Contracts
{
    public interface IRepository<T>
        where T : IModel
    {
        T Get(string id);
        IEnumerable<T> GetAll();
        void Update(T model);
        T Add(T model);
        void AddMany(IEnumerable<T> models);
        void Delete(string id);
    }
}

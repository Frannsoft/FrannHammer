using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.DataAccess.Contracts
{
    public interface IRepository<T>
        where T : IModel
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        T Update(T model);
        void Delete(T model);
        T Add(T model);
        void AddMany(IEnumerable<T> models);
    }
}

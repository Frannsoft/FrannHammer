using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.DataAccess.Contracts
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> 
        where T : class, IModel
    { }

    public interface IReadRepository<out T>
        where T : class, IModel
    {
        T Get(int id);
        IEnumerable<T> GetAll();
    }

    public interface IWriteRepository<in T>
        where T : class, IModel
    {
        void Update(T model);
        void Add(T model);
        void AddMany(IEnumerable<T> models);
        void Delete(T model);
    }
}

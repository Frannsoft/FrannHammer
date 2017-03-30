using FrannHammer.Domain.Contracts;

namespace FrannHammer.DataAccess.Contracts
{
    public interface IRepository<T>
        where T : IModel
    {
        T Get(int id);
        T Update(T model);
        void Delete(T model);
        T Add(T model);
    }
}

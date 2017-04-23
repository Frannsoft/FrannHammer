using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface ICrudService<T>
        where T : IModel
    {
        T Get(int id, string fields = "");
        IEnumerable<T> GetAll(string fields = "");
        T Add(T character);
        void AddMany(IEnumerable<T> models);
    }
}
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface ICrudService<T> : IWriterService<T>, IReaderService<T>
        where T : IModel
    { }

    public interface IWriterService<in T>
        where T : IModel
    {
        void Add(T character);
        void AddMany(IEnumerable<T> models);
    }

    public interface IReaderService<out T>
        where T : IModel
    {
        T Get(string id, string fields = "");
        IEnumerable<T> GetAll(string fields = "");
    }
}
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IWriterService<in T>
        where T : IModel
    {
        void Add(T character);
        void AddMany(IEnumerable<T> models);
    }
}
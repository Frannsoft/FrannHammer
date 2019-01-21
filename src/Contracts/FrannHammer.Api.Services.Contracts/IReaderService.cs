using FrannHammer.Domain.Contracts;
using System;
using System.Collections.Generic;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IReaderService<out T>
        where T : IModel
    {
        T GetSingleWhere(Func<T, bool> where);
        IEnumerable<T> GetAllWhere(Func<T, bool> where);
        IEnumerable<T> GetAll();
        T GetSingleByInstanceId(string id);
        IEnumerable<T> GetAllWhereName(string name);
    }
}
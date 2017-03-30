using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IDataRetrievalService
    {
        T Get<T>(int id) where T : IModel;
        IEnumerable<T> GetAll<T>() where T : IModel;
        bool EntityExists<T>(int id) where T : IModel;
    }
}

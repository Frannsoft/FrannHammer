using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IMovementService
    {
        IMovement Get(int id, string fields = "");
        IEnumerable<IMovement> GetAll(string fields = "");
    }
}

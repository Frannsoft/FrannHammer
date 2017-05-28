using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services
{
    public class DefaultMovementService : OwnerBasedApiService<IMovement>, IMovementService
    {
        public DefaultMovementService(IRepository<IMovement> repository)
            : base(repository)
        { }
    }
}

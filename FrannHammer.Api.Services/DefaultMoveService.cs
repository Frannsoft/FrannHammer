using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Api.Services.Contracts;

namespace FrannHammer.Api.Services
{
    public class DefaultMoveService : BaseApiService<IMove>, IMoveService
    {
        public DefaultMoveService(IRepository<IMove> repository)
            : base(repository)
        { }
    }
}

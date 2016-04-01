using Kurogane.Data.RestApi.Models;

namespace Kurogane.Data.RestApi.Infrastructure
{
    public interface ISmashAttributeRepository : IRepository<SmashAttributeType>
    {

    }

    public class SmashAttributeRepository : RepositoryBase<SmashAttributeType>, ISmashAttributeRepository
    {
        public SmashAttributeRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }
    }
}
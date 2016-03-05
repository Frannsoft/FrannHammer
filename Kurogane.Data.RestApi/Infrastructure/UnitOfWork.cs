
namespace Kurogane.Data.RestApi.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private Sm4ShEntities _dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public Sm4ShEntities DbContext => _dbContext ?? (_dbContext = _dbFactory.Init());

        public void Commit()
        {
            DbContext.Commit();
        }
    }
}

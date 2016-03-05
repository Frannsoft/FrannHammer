
namespace Kurogane.Data.RestApi.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        Sm4ShEntities _dbContext;

        public Sm4ShEntities Init()
        {
            return _dbContext ?? (_dbContext = new Sm4ShEntities());
        }

        protected override void DisposeCore()
        {
            _dbContext?.Dispose();
        }
    }
}


namespace KuroganeHammer.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        Sm4shEntities dbContext;

        public Sm4shEntities Init()
        {
            return dbContext ?? (dbContext = new Sm4shEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}

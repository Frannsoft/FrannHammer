using System;

namespace KuroganeHammer.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        Sm4shEntities Init();
    }
}

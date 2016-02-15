using System;

namespace Kurogane.Data.RestApi.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        Sm4shEntities Init();
    }
}

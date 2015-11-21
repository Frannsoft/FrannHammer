using Kurogane.Data.RestApi.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http.Dispatcher;

namespace Kurogane.Data.RestApi
{
    public class CustomAssembliesResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            var baseAssemblies = base.GetAssemblies().ToList();
            var assemblies = new List<Assembly>(baseAssemblies);
            assemblies.Add(typeof(CharacterController).Assembly);
            assemblies.Add(typeof(MoveController).Assembly);
            assemblies.Add(typeof(MovementController).Assembly);
            baseAssemblies.AddRange(assemblies);

            return baseAssemblies.Distinct().ToList();
        }
    }
}
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace FrannHammer.WebApi.SwaggerExtensions
{
    public class SwaggerAccessDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            foreach (var pathItem in swaggerDoc.paths.Values)
            {
                pathItem.delete = null;
                //pathItem.get = NullifyIfRestricted(pathItem.get);
                pathItem.head = null;
                pathItem.options = null;
                pathItem.patch = null;
                pathItem.post = null;
                pathItem.put = null; 
            }
        }
    }
}
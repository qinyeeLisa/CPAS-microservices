using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PermitApplicationWebApi.Services
{
    public class CustomerSwaggerFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var nonMobileRoutes = swaggerDoc.Paths
                .Where(x => x.Key.ToLower().Contains("user"))
                .ToList();
            nonMobileRoutes.ForEach(x => { swaggerDoc.Paths.Remove(x.Key); });
        }
    }
}

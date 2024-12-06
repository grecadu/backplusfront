using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Employee.Filters;

public class AddApiKeyHeaderParameter : IOperationFilter
{
  public void Apply(OpenApiOperation operation, OperationFilterContext context)
  {

    var hasApiKeyAuthFilter = context.ApiDescription.CustomAttributes()
      .Any(attr => attr.GetType() == typeof(ServiceFilterAttribute) && ((ServiceFilterAttribute)attr).ServiceType == typeof(ApiKeyAuthFilter));


    if (hasApiKeyAuthFilter)
    {
      if (operation.Parameters == null)
        operation.Parameters = new List<OpenApiParameter>();

      operation.Parameters.Add(new OpenApiParameter
      {
        Name = "x-api-key",
        In = ParameterLocation.Header,
        Description = "API Key",
        Required = true
      });
    }
  }

}

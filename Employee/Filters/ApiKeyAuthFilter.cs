
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Filters;
public class ApiKeyAuthFilter : IAuthorizationFilter
{
  private readonly IConfiguration _configuration;

  public ApiKeyAuthFilter(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public void OnAuthorization(AuthorizationFilterContext context)
  {
    if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extractedApiKey))
    {
      context.HttpContext.Response.StatusCode = 401;
      context.Result = new JsonResult("API Key missing");
      return;
    }

    var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
    if (!apiKey.Equals(extractedApiKey))
    {
      context.HttpContext.Response.StatusCode = 401;
      context.Result = new JsonResult("API Key invalid");
      return;
    }
  }
}

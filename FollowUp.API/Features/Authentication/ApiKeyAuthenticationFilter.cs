using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FollowUp.API.Features.Authentication;

public class ApiKeyAuthenticationFilter : IAuthorizationFilter
{
    private readonly IConfiguration _configuration;
    
    public ApiKeyAuthenticationFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(AuthenticationConstants.APiKeyHeaderName,
                out var providedKey))
        {
            context.Result = new UnauthorizedObjectResult("Missing API Key");
            return;
        }

        string apiKey = _configuration.GetValue<string>(AuthenticationConstants.APIKeySettingsName)!;
        if (!apiKey.Equals(providedKey))
        {
            context.Result = new UnauthorizedObjectResult("Invalid API Key");
            return;
        }
    }
}
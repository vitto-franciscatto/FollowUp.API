namespace FollowUp.API.Features.Authentication;

public static class RegisterService
{
    public static IServiceCollection ConfigureApiKeyAuthentication(this IServiceCollection services)
    {
        services.AddScoped<ApiKeyAuthenticationFilter>();
        
        return services;
    }
}
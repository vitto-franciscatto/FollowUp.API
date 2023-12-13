using Microsoft.OpenApi.Models;

namespace FollowUp.API.Features.Swagger;

public static class RegisterService
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "FollowUps API", Version = "1" });
            c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Name = "authentication-key",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Description = "Provided key for API usage",
                Scheme = "ApiKeyScheme"
            });

            var key = new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                In = ParameterLocation.Header
            };

            var requirement = new OpenApiSecurityRequirement
            {
                { key, new List<string>()}
            };

            c.AddSecurityRequirement(requirement);
        });
        
        return services;
    }
}
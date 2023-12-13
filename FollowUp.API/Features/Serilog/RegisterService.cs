using Serilog;

namespace FollowUp.API.Features.Serilog;

public static class RegisterService
{
    public static void ConfigureSerilog(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddSingleton(serviceProvider =>
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .CreateLogger();

            return Log.Logger;
        });
    }
}
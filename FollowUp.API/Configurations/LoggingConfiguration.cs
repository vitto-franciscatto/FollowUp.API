using Serilog;

namespace FollowUp.API.Configurations;

public static class LoggingConfiguration
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
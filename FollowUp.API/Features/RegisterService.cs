using System.Reflection;

namespace FollowUp.API.Features
{
    public static class RegisterService
    {
        public static void ConfigureFeatures(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddMediatR(_ => 
                _.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}

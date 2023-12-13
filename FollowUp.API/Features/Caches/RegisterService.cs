namespace FollowUp.API.Features.Caches
{
    public static class RegisterService
    {
        public static void ConfigureCaches(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDistributedMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();
        }
    }
}

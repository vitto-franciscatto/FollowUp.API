namespace FollowUp.API.Features.Tags
{
    public static class RegisterService
    {
        public static void ConfigureTags(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddScoped<ITagRepository, TagRepository>();
        }
    }
}

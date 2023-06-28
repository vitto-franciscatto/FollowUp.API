using FollowUp.Application.Interfaces;
using FollowUp.Infra.Data.Context;
using FollowUp.Infra.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FollowUp.Infra
{
    public static class RegisterService
    {
        public static void ConfigureInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FollowUpDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IFollowUpRepository, FollowUpRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
        }
    }
}

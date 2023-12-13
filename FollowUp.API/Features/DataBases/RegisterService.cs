using FollowUp.API.Features.DataBases.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FollowUp.API.Features.DataBases
{
    public static class RegisterService
    {
        public static void ConfigureDataBases(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<FollowUpDbContext>(options => 
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
        }
    }
}

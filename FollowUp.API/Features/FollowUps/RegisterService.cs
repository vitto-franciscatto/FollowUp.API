using FluentValidation;
using FollowUp.API.Features.FollowUps.CreateFollowUp;
using FollowUp.API.Features.Tags.CreateTag;

namespace FollowUp.API.Features.FollowUps
{
    public static class RegisterService
    {
        public static void ConfigureFollowUps(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddScoped<
                IValidator<AuthorDTO>, 
                AuthorDTOValidator>();
            
            services.AddScoped<
                IValidator<ContactDTO>, 
                ContactDTOValidator>();
            
            services.AddScoped<
                IValidator<CreateFollowUpCommand>, 
                CreateFollowUpCommandValidator>();

            services.AddScoped<
                IValidator<CreateTagCommand>, 
                CreateTagCommandValidator>();
            
            services.AddScoped<IFollowUpRepository, FollowUpRepository>();
        }
    }
}

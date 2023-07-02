using FluentValidation;
using FollowUp.Application.Commands.CreateFollowUp;
using FollowUp.Application.Commands.CreateTag;
using FollowUp.Application.DTOs;
using FollowUp.Application.Interfaces;
using FollowUp.Application.Services;
using FollowUp.Application.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FollowUp.Application
{
    public static class RegisterService
    {
        public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(_ => _.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<IValidator<AuthorDTO>, AuthorDTOValidator>();
            services.AddScoped<IValidator<ContactDTO>, ContactDTOValidator>();
            services.AddScoped<IValidator<CreateFollowUpCommand>, CreateFollowUpCommandValidator>();
            services.AddScoped<IValidator<CreateTagCommand>, CreateTagCommandValidator>();

            services.AddDistributedMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();
        }
    }
}

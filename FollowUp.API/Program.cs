using FollowUp.API.Features;
using FollowUp.API.Features.Authentication;
using FollowUp.API.Features.Caches;
using FollowUp.API.Features.DataBases;
using FollowUp.API.Features.FollowUps;
using FollowUp.API.Features.Serilog;
using FollowUp.API.Features.Swagger;
using FollowUp.API.Features.Tags;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

services.ConfigureSerilog(configuration);

services.ConfigureFeatures(configuration);
services.ConfigureCaches(configuration);
services.ConfigureDataBases(configuration);
services.ConfigureFollowUps(configuration);
services.ConfigureTags(configuration);

services.ConfigureApiKeyAuthentication();
services.AddControllers();

services.AddEndpointsApiExplorer();
services.ConfigureSwagger();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

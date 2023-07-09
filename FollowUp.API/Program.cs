using FollowUp.API.Authentication;
using FollowUp.API.Swagger;
using FollowUp.Application;
using FollowUp.Infra;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

services.ConfigureInfra(configuration);
services.ConfigureApplication(configuration);

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

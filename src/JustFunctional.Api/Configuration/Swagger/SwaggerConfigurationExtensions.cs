using Microsoft.OpenApi.Models;
using System.Reflection;

namespace JustFunctional.Api.Configuration
{
    public static class SwaggerConfigurationExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            return services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "An ASP.NET Core Web API for managing ToDo items",
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            return app
                .UseSwagger()
                .UseSwaggerUI();
        }
    }
}

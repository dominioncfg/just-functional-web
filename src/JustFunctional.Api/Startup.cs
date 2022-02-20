using JustFunctional.Api.Configuration;

namespace JustFunctional.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddJustFunctional()
                .AddCustomProblemDetails()
                .AddCustomSwagger()
                
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app)
        {
            app
                .UseCustomProblemDetails()
                .UseCustomSwagger()
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                });
        }
    }
}
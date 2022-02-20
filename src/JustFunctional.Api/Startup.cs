using JustFunctional.Api.Configuration;

namespace JustFunctional.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddJustFunctional()
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .ConfigureProblemDetails()
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCustomProblemDetails();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
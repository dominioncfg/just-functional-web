namespace JustFunctional.Api.Configuration;

public static class CorsConfigurationExtensions
{
    private const string PolicyName = "AllowsAllPolicy";
    public static IServiceCollection AddCustomCors(this IServiceCollection services)

    {
        services.AddCors(options =>
        {
            options.AddPolicy(PolicyName,
                              policy =>
                              {
                                  policy
                                    .WithOrigins("*");
                              });
        });

        return services;
    }
    public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
    {
        return app.UseCors(PolicyName);
    }
}
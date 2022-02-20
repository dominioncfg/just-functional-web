using JustFunctional.Core;

namespace JustFunctional.Api.Configuration
{
    public static class JustFunctionalConfigurationExtensions
    {
        public static IServiceCollection AddJustFunctional(this IServiceCollection services)
        {
            var factory = FunctionFactoryBuilder.ConfigureFactory(options =>
            {
                options
                    .WithEvaluationContextVariablesProvider()
                    .WithDefaultsTokenProvider()
                    .WithCompiledEvaluator();
            });
            services.AddSingleton(factory);
            return services;
        }
    }
}
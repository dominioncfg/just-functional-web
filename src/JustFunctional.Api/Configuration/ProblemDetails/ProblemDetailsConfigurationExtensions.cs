using Hellang.Middleware.ProblemDetails;
using JustFunctional.Core;
using Microsoft.AspNetCore.Mvc;

namespace JustFunctional.Api.Configuration
{
    internal static class ProblemDetailsConfigurationExtensions
    {
        private static string ValidationErrorMessage => "Please refer to the errors property for additional details.";
        private static string ErrorJsonContentType => "application/problem+json";
        private static string ErrorXmlContentType => "application/problem+xml";


        public static IServiceCollection ConfigureProblemDetails(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ProblemDetailsApiBehaviorConfiguration;
            });
            services.AddProblemDetails(opts =>
            {
                opts.IncludeExceptionDetails = (_, __) => false;
                opts.MapCustomExceptions();
                opts.Map<Exception>(ex => ApiExceptionHandlers.UnhandledExceptionHandler(ex));
            });
            return services;
        }

        private static void MapCustomExceptions(this ProblemDetailsOptions opts)
        {
            opts.Map<JustFunctionalBaseException>(ex => ApiExceptionHandlers.JustFunctionalBaseExceptionHandler(ex));
        }

        public static IApplicationBuilder UseCustomProblemDetails(this IApplicationBuilder app)
        {
            app.UseProblemDetails();
            return app;
        }

        private static IActionResult ProblemDetailsApiBehaviorConfiguration(ActionContext context)
        {
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Instance = context.HttpContext.Request.Path,
                Status = StatusCodes.Status400BadRequest,
                Type = $"https://httpstatuses.com/400",
                Detail = ValidationErrorMessage
            };
            return new BadRequestObjectResult(problemDetails)
            {
                ContentTypes = {
                    ErrorJsonContentType,
                    ErrorXmlContentType
                }
            };
        }
    }
}

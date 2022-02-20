using JustFunctional.Core;
using Microsoft.AspNetCore.Mvc;

namespace JustFunctional.Api.Configuration
{
    internal static class ApiExceptionHandlers
    {
        private static string UnhandledExceptionTitle => "Whoops. Something went wrong";
        private static string JustFunctionalBaseExceptionTitle => "Looks like there is a problem with your request";

        public static ProblemDetails JustFunctionalBaseExceptionHandler(JustFunctionalBaseException ex)
        {
            return new ProblemDetails
            {
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest,
                Title = JustFunctionalBaseExceptionTitle
            };
        }

        public static ProblemDetails UnhandledExceptionHandler(Exception ex)
        {
            return new ProblemDetails
            {
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError,
                Title = UnhandledExceptionTitle
            };
        }
    }
}

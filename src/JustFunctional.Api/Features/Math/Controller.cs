using JustFunctional.Core;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JustFunctional.Api.Features.Math
{
    [ApiController]
    [Route("api/v2/math")]
    public class MathController : ControllerBase
    {
        private readonly IFunctionFactory _functionFactory;
        public MathController(IFunctionFactory functionFactory)
        {
            _functionFactory = functionFactory;
        }

        /// <summary>
        /// Validates if a given mathematical expression is syntactically correct expression.
        /// </summary>
        /// <param name="request">Expression and variables (variables are optional).</param>
        /// <returns>Returns if the expression is correct.</returns>
        /// <response code="200">Returns an object indicating if the expression if correct and in case it isn`t a list of errors describing the issue.</response>
        /// <response code="400">Something in the way the request was made is wrong.</response>            
        [HttpGet("validate")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ValidationApiResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ProblemDetails))]
        public ValidationApiResponse Validate([FromQuery] ValidationApiRequest request)
        {
            var allowedVariables = new PredefinedVariablesProvider(request.Variables ?? Array.Empty<string>());
            var result = _functionFactory.TryCreate(request.Expression ?? string.Empty, allowedVariables);

            return new ValidationApiResponse()
            {
                Success = result.Success,
                Errors = result.Errors,
            };
        }

        /// <summary>
        /// Evaluates given mathematical expression.
        /// </summary>
        /// <param name="request">Expression and variables with values (variables are optional).</param>
        /// <returns>Returns the result of evaluating the expression.</returns>
        /// <response code="200">Returns the result of evaluating the expression.</response>
        /// <response code="400">The expression is not syntactically correct or there is something wrong with the request.</response>
        [HttpGet("evaluate")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(EvaluationApiResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ProblemDetails))]
        public EvaluationApiResponse Evaluate([FromQuery] EvaluationApiRequest request)
        {
            var fx = _functionFactory.Create(request.Expression);
            var result = fx.Evaluate(new EvaluationContext(request.Variables ?? new Dictionary<string, decimal>()));
            return new EvaluationApiResponse()
            {
                Result = result,
            };
        }
    }
}

using JustFunctional.Core;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("validate")]
        public IActionResult Validate([FromQuery] ValidationApiRequest request)
        {
            var allowedVariables = new PredefinedVariablesProvider(request.Variables ?? Array.Empty<string>());
            var result = _functionFactory.TryCreate(request.Expression, allowedVariables);

            return Ok(new ValidationApiResponse()
            {
                Success = result.Success,
                Errors = result.Errors,
            });
        }

        [HttpGet("evaluate")]
        public IActionResult Evaluate([FromQuery] EvaluationApiRequest request)
        {
            var fx = _functionFactory.Create(request.Expression);
            var result = fx.Evaluate(new EvaluationContext(request.Variables ?? new Dictionary<string, decimal>()));
            return Ok(new EvaluationApiResponse()
            {
                Result = result,
            });
        }
    }
}

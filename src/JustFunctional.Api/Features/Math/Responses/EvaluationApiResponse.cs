namespace JustFunctional.Api.Features.Math
{
    /// <summary>
    /// Response from evaulating a mathematical expression
    /// </summary>
    public record EvaluationApiResponse
    {
        /// <summary>
        /// The result of the expression.
        /// </summary>
        public decimal Result { get; init; }
    }
}
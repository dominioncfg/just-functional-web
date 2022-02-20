namespace JustFunctional.Api.Features.Math
{
    /// <summary>
    /// Request for evaluating a mathematical expression
    /// </summary>
    public record EvaluationApiRequest
    {
        /// <summary>
        /// The expression to evaluate.
        /// </summary>
        public string Expression { get; init; } = string.Empty;
        
        /// <summary>
        /// The expression variables if any.
        /// </summary>
        public Dictionary<string, decimal>? Variables { get; init; }
    }
}

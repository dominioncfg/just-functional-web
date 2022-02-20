namespace JustFunctional.Api.Features.Math
{
    /// <summary>
    /// Request for validating a mathematical expression
    /// </summary>
    public record ValidationApiRequest
    {
        /// <summary>
        /// The expression to validate.
        /// </summary>
        public string Expression { get; init; } = string.Empty;


        /// <summary>
        /// The expression variables if any.
        /// </summary>
        public string[]? Variables { get; init; }
    }
}

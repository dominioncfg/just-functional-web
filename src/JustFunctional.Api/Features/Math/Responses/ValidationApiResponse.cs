namespace JustFunctional.Api.Features.Math
{
    /// <summary>
    /// Response from validating a mathematical expression
    /// </summary>
    public record ValidationApiResponse
    {
        /// <summary>
        /// True if the expression is syntactically correct, false otherwise.
        /// </summary>
        public bool Success { get; init; }

        /// <summary>
        /// If the expression is not correct this list will contain the reasons why.
        /// </summary>
        public IEnumerable<string> Errors { get; init; } = Enumerable.Empty<string>();
    }
}
namespace JustFunctional.Api.Features.Math
{
    public record EvaluationApiRequest
    {
        public string Expression { get; init; } = string.Empty;
        public Dictionary<string, decimal>? Variables { get; init; }
    }
}

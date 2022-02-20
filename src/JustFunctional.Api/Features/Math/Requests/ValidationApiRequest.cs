namespace JustFunctional.Api.Features.Math
{
    public record ValidationApiRequest
    {
        public string Expression { get; init; } = string.Empty;
        public string[]? Variables { get; init; }
    }
}

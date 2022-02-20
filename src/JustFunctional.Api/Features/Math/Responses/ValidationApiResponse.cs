namespace JustFunctional.Api.Features.Math
{
    public record ValidationApiResponse
    {
        public bool Success { get; init; }
        public IEnumerable<string> Errors { get; init; } = Enumerable.Empty<string>();
    }
}
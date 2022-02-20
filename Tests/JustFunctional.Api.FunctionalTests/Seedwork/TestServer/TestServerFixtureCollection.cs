using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace JustFunctional.Api.FunctionalTests.Seedwork
{
    [ExcludeFromCodeCoverage]
    [CollectionDefinition(nameof(TestServerFixtureCollection))]
    public class TestServerFixtureCollection : ICollectionFixture<TestServerFixture> { }
}

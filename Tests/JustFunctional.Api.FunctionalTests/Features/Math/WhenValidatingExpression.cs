using FluentAssertions;
using JustFunctional.Api.Features.Math;
using JustFunctional.Api.FunctionalTests.Seedwork;
using System;
using System.Threading.Tasks;
using System.Web;
using Xunit;

namespace JustFunctional.Api.FunctionalTests.Features.Math
{
    [Collection(nameof(TestServerFixtureCollection))]
    public class WhenValidatingExpressions
    {
        private readonly TestServerFixture Given;
        public WhenValidatingExpressions(TestServerFixture given)
        {
            Given = given ?? throw new Exception("Null Server");
        }

        [Fact]
        public async Task ValidationReturnsTrueWhenExpressionIsValidConstantExpression()
        {
            const string fx = "3+2";

            var apiResult = await GetAndValidateExpression(fx);

            apiResult.Should().NotBeNull();
            apiResult.Success.Should().BeTrue();
            apiResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public async Task ValidationReturnsTrueWhenExpressionIsValidSingleVariable()
        {
            const string fx = "X+2";
            string[] variables = new[] { "X" };

            var apiResult = await GetAndValidateExpression(fx, variables);

            apiResult.Should().NotBeNull();
            apiResult.Success.Should().BeTrue();
            apiResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public async Task ValidationReturnsTrueWhenExpressionIsValidMultipleVariables()
        {
            const string fx = "X+Y";
            string[] variables = new[] { "X", "Y" };

            var apiResult = await GetAndValidateExpression(fx, variables);

            apiResult.Should().NotBeNull();
            apiResult.Success.Should().BeTrue();
            apiResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public async Task ValidationReturnsFalseWhenExpressionIsEmpty()
        {
            const string fx = "";

            var apiResult = await GetAndValidateExpression(fx);

            apiResult.Should().NotBeNull();
            apiResult.Success.Should().BeFalse();
            apiResult.Errors.Should().NotBeEmpty();
            foreach (var error in apiResult.Errors)
                error.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ValidationReturnsFalseWhenExpressionIsInvalidSyntaxError()
        {
            const string fx = "(3+2";

            var apiResult = await GetAndValidateExpression(fx);

            apiResult.Should().NotBeNull();
            apiResult.Success.Should().BeFalse();
            apiResult.Errors.Should().NotBeEmpty();
            foreach (var error in apiResult.Errors)
                error.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ValidationReturnsFalseWhenExpressionIsInvalidMissingVariable()
        {
            const string fx = "X+2";

            var apiResult = await GetAndValidateExpression(fx);

            apiResult.Should().NotBeNull();
            apiResult.Success.Should().BeFalse();
            apiResult.Errors.Should().NotBeEmpty();
            foreach (var error in apiResult.Errors)
                error.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ValidationReturnsFalseWhenExpressionIsInvalidWrongVariable()
        {
            const string fx = "X+2";
            string[] variables = new[] { "Y" };
            var apiResult = await GetAndValidateExpression(fx, variables);

            apiResult.Should().NotBeNull();
            apiResult.Success.Should().BeFalse();
            apiResult.Errors.Should().NotBeEmpty();
            foreach (var error in apiResult.Errors)
                error.Should().NotBeEmpty();
        }

        private async Task<ValidationApiResponse> GetAndValidateExpression(string expression, string[]? variables = null)
        {
            var url = ApiHelper.Get.GetExpressionValidationUrl(expression, variables);
            return await Given.Server.CreateClient().GetAsync<ValidationApiResponse>(url);
        }

        private static class ApiHelper
        {
            public static class Get
            {
                public static string GetExpressionValidationUrl(string expression, string[]? variables = null)
                {
                    var baseUrl = "/api/v2/math/validate";
                    var queryParams = HttpUtility.ParseQueryString(string.Empty);

                    queryParams["expression"] = expression;
                    if(variables is not null)
                    {
                        foreach (var variable in variables)
                        {
                            queryParams.Add("variables", variable);
                        }
                    }
                    
                    return $"{baseUrl}?{queryParams}";
                }
            }
        }
    }
}

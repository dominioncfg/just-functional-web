using FluentAssertions;
using JustFunctional.Api.Features.Math;
using JustFunctional.Api.FunctionalTests.Seedwork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Xunit;

namespace JustFunctional.Api.FunctionalTests.Features.Math
{
    [Collection(nameof(TestServerFixtureCollection))]
    public class WhenEvaluatingExpressions
    {
        private readonly TestServerFixture Given;
        public WhenEvaluatingExpressions(TestServerFixture given)
        {
            Given = given ?? throw new Exception("Null Server");
        }

        [Fact]
        public async Task EvaluationReturnsOkWhenExpressionIsValidConstantExpression()
        {
            const string fx = "3+2";

            var apiResult = await GetAndEvaluateExpressionAsync(fx);

            apiResult.Should().NotBeNull();
            apiResult.Result.Should().Be(5);
        }

        [Fact]
        public async Task EvaluationReturnsOkWhenExpressionIsValidSingleVariable()
        {
            const string fx = "X+2";
            var variables = new Dictionary<string, decimal> { ["X"] = 3 };

            var apiResult = await GetAndEvaluateExpressionAsync(fx, variables);

            apiResult.Should().NotBeNull();
            apiResult.Result.Should().Be(5);
        }

        [Fact]
        public async Task EvaluationReturnsOkWhenExpressionIsValidMultipleVariables()
        {
            const string fx = "X+Y";
            var variables = new Dictionary<string, decimal> { ["X"] = 3, ["Y"] = 8 };

            var apiResult = await GetAndEvaluateExpressionAsync(fx, variables);

            apiResult.Should().NotBeNull();
            apiResult.Result.Should().Be(11);
        }

        [Fact]
        public async Task EvaluationReturnsBadRequestWhenExpressionIsEmpty()
        {
            const string fx = "";
            await GetAndExpectBadRequestAsync(fx);
        }

        [Fact]
        public async Task EvaluationReturnsBadRequestWhenExpressionIsInvalidSyntaxError()
        {
            const string fx = "(3+2";
            await GetAndExpectBadRequestAsync(fx);
        }
        
        [Fact]
        public async Task EvaluationReturnsBadRequestWhenExpressionIsInvalidMissingVariable()
        {
            const string fx = "X+2";
            await GetAndExpectBadRequestAsync(fx);
        }
        
        [Fact]
        public async Task EvaluationReturnsBadRequestWhenExpressionIsInvalidWrongVariable()
        {
            const string fx = "X+2";
            var variables = new Dictionary<string, decimal> { ["Y"] = 3 };
            await GetAndExpectBadRequestAsync(fx,variables);
        }

        private async Task<EvaluationApiResponse> GetAndEvaluateExpressionAsync(string expression, Dictionary<string, decimal>? variables = null)
        {
            var url = ApiHelper.Get.GetExpressionEvaluationUrl(expression, variables);
            return await Given.Server.CreateClient().GetAsync<EvaluationApiResponse>(url);
        }
        private async Task GetAndExpectBadRequestAsync(string expression, Dictionary<string, decimal>? variables = null)
        {
            var url = ApiHelper.Get.GetExpressionEvaluationUrl(expression, variables);
            await Given.Server.CreateClient().GetAndExpectBadRequestAsync(url);
        }

        private static class ApiHelper
        {
            public static class Get
            {
                public static string GetExpressionEvaluationUrl(string expression, Dictionary<string, decimal>? variables)
                {
                    var baseUrl = "/api/v2/math/evaluate";
                    var queryParams = HttpUtility.ParseQueryString(string.Empty);

                    queryParams["expression"] = expression;
                    if (variables is not null)
                    {
                        foreach (var variable in variables)
                        {
                            queryParams.Add($"variables[{variable.Key}]", variable.Value.ToString());
                        }
                    }

                    return $"{baseUrl}?{queryParams}";
                }
            }
        }
    }
}

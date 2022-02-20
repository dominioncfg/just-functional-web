using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace JustFunctional.Api.FunctionalTests.Seedwork
{
    public static class HttpClientExtensions
    {
        #region Get
        public static async Task<T> GetAsync<T>(this HttpClient client, string url)
        {
            using var postResponse = await client.GetAsync(url);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            postResponse.IsSuccessStatusCode.Should().BeTrue();
            var responseModel = await postResponse.Deserialize<T>();
            responseModel.Should().NotBeNull();
            return responseModel;
        }

        public static async Task GetAndExpectBadRequestAsync(this HttpClient client, string url)
        {
            using var postResponse = await client.GetAsync(url);
            postResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            postResponse.IsSuccessStatusCode.Should().BeFalse();
            var responseModel = await postResponse.Deserialize<ProblemDetails>();
            responseModel.Should().NotBeNull();
        }    
        #endregion


   
    }
}

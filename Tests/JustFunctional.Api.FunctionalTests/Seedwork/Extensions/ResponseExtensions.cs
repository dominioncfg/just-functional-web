using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JustFunctional.Api.FunctionalTests.Seedwork
{
    public static class ResponseExtensions
    {
        public async static Task<T> Deserialize<T>(this HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var result = JsonSerializer.Deserialize<T>(content, options);

            if (result is null)
                throw new System.Exception("The Response could not be parsed.");

            return result;
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System;

namespace JustFunctional.Api.FunctionalTests.Seedwork
{
    public sealed class TestServerFixture : IDisposable
    {
        public TestServer Server { get; }
        public TestServerFixture()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {

                })
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.UseStartup<Startup>();
                });


            var host = hostBuilder.StartAsync().GetAwaiter().GetResult();
            Server = host.GetTestServer();
        }
        public void Dispose()
        {
            Server.Dispose();
        }
    }
}

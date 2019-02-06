using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrannHammer.NetCore.WebApi.Tests
{
    /// <summary>
    /// Singleton instance of the netcore.webapi. Uses the Startup logic from netcore.webapi.
    /// </summary>
    public class IntegrationTestServer : IDisposable
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        static IntegrationTestServer()
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>().ConfigureTestServices(services =>
            {
                services.AddTransient(sp =>
                {
                    return new DebugStorageLocationResolver(
                        "../../../../../localstorage/character.json",
                        "../../../../../localstorage/move.json",
                        "../../../../../localstorage/movement.json",
                        "../../../../../localstorage/attribute.json",
                        "../../../../../localstorage/unique.json"
                        );
                });
            }).UseEnvironment("development"));
            _httpClient = _testServer.CreateClient();
        }

        public async Task<HttpResponseMessage> GetAsync(string url) => await _httpClient.GetAsync(url);

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _httpClient.Dispose();
                    _testServer.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}

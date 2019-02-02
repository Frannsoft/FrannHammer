using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrannHammer.NetCore.WebApi.Tests
{
    public class IntegrationTestServer : IDisposable
    {
        private HttpClient _httpClient;
        private TestServer _testServer;

        public IntegrationTestServer()
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>().UseEnvironment("development"));
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

using FrannHammer.Domain;
using FrannHammer.NetCore.WebApi;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FrannHammer.WebApi.Tests
{
    [TestFixture]
    public class ParallelRequestTests
    {
        [Test]
        public async Task ExecuteSingleGET()
        {
            using (var httpClient = new HttpClient())
            {
                var sw = new Stopwatch();
                sw.Start();
                var response = await httpClient.GetAsync("https://core-test-khapi.azurewebsites.net/api/characters/1/moves");
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                sw.Stop();
                var parsedContent = JsonConvert.DeserializeObject<List<Move>>(content);
                Console.WriteLine($"{sw.Elapsed.Milliseconds}");
                Assert.That(parsedContent[0].Owner, Is.EqualTo("Bayonetta"));
            }
        }

        [Test]
        public async Task ExecuteSingleGETonCurrentProd()
        {
            using (var httpClient = new HttpClient())
            {
                var sw = new Stopwatch();
                sw.Start();
                var response = await httpClient.GetAsync("http://beta-api-kuroganehammer.azurewebsites.net/api/characters/1/moves");
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                sw.Stop();
                var parsedContent = JsonConvert.DeserializeObject<List<Move>>(content);
                Console.WriteLine($"{sw.Elapsed.Milliseconds}");
                Assert.That(parsedContent[0].Owner, Is.EqualTo("Bayonetta"));
            }
        }

        [Test]
        public async Task ExecuteGETsInParallel()
        {
            var testServer = new TestServer(WebHost.CreateDefaultBuilder<Startup>(null));
            var httpClient = testServer.CreateClient();

            var semaphoreSlim = new SemaphoreSlim(4);
            var times = Enumerable.Range(0, 100);

            var trackedTasks = new List<Task>();

            foreach (var time in times)
            {
                //await semaphoreSlim.WaitAsync();
                trackedTasks.Add(Task.Run(async () =>
                {
                    var response = await httpClient.GetAsync("api/characters/name/bowser/moves");
                    response.EnsureSuccessStatusCode();

                    string content = await response.Content.ReadAsStringAsync();

                    var parsedContent = JsonConvert.DeserializeObject<List<Move>>(content);
                    Console.WriteLine($"request # {time} -> {parsedContent[0].Owner}");
                    Assert.That(parsedContent[0].Owner, Is.EqualTo("Bowser"));
                    semaphoreSlim.Release();
                }));
            }

            await Task.WhenAll(trackedTasks);
        }

        [Test]
        public async Task ExecuteGETsInParallelOnTestEnvironment()
        {
            using (var httpClient = new HttpClient())
            {
                var semaphoreSlim = new SemaphoreSlim(4);
                var times = Enumerable.Range(0, 50);

                var trackedTasks = new List<Task>();

                foreach (var time in times)
                {
                    //await semaphoreSlim.WaitAsync();
                    trackedTasks.Add(Task.Run(async () =>
                    {
                        var sw = new Stopwatch();
                        sw.Start();
                        var response = await httpClient.GetAsync("https://core-test-khapi.azurewebsites.net/api/characters/1/moves");
                        response.EnsureSuccessStatusCode();

                        string content = await response.Content.ReadAsStringAsync();

                        sw.Stop();
                        var parsedContent = JsonConvert.DeserializeObject<List<Move>>(content);
                        Console.WriteLine($"{sw.Elapsed.Milliseconds}");
                        Assert.That(parsedContent[0].Owner, Is.EqualTo("Bayonetta"));
                        semaphoreSlim.Release();
                    }));
                }

                await Task.WhenAll(trackedTasks);
            }
        }

        [Test]
        public async Task ExecuteGETsInParallelOnCurrentProdEnvironment()
        {
            using (var httpClient = new HttpClient())
            {
                var semaphoreSlim = new SemaphoreSlim(4);
                var times = Enumerable.Range(0, 50);

                var trackedTasks = new List<Task>();

                foreach (var time in times)
                {
                    //await semaphoreSlim.WaitAsync();
                    trackedTasks.Add(Task.Run(async () =>
                    {
                        var sw = new Stopwatch();
                        sw.Start();
                        var response = await httpClient.GetAsync("http://beta-api-kuroganehammer.azurewebsites.net/api/characters/1/moves");
                        response.EnsureSuccessStatusCode();

                        string content = await response.Content.ReadAsStringAsync();

                        sw.Stop();
                        var parsedContent = JsonConvert.DeserializeObject<List<Move>>(content);
                        Console.WriteLine($"{sw.Elapsed.Milliseconds}");
                        Assert.That(parsedContent[0].Owner, Is.EqualTo("Bayonetta"));
                        semaphoreSlim.Release();
                    }));
                }

                await Task.WhenAll(trackedTasks);
            }
        }
    }
}

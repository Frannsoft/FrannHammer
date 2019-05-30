using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace FrannHammer.NetCore.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
#if !DEBUG
#endif
                .UseStartup<Startup>();
    }
}

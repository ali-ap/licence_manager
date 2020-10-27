using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Licence.Service.Registration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
    
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseKestrel(options =>
                        {
                            options.ListenAnyIP(Int32.Parse(System.Environment.GetEnvironmentVariable("PORT")));
                        })
                        //.UseUrls("http://*:6000")
                        .UseStartup<Startup>();
                });
        
    }
}

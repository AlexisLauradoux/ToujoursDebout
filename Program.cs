using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using JamaisCouché;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ToujoursDebout.Services;

namespace ToujoursDebout
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.RunAsync();
            Console.WriteLine("- Server started -");
            ToujoursDeboutService jamaisCouche = new ToujoursDeboutService();
            Console.WriteLine("- Client started -");
            jamaisCouche.SendMessage();
            host.StopAsync();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

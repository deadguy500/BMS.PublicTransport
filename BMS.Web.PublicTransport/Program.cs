using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace BMS.Web.PublicTransport
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var apiKey = Environment.GetEnvironmentVariable("VasttrafikKey");
            var deviceId = Environment.GetEnvironmentVariable("VasttrafikDeviceId");

            if (string.IsNullOrEmpty(apiKey))
            {
                Console.WriteLine("ApiKey is missing");
                return;
            }

            if (string.IsNullOrEmpty(deviceId))
            {
                Console.WriteLine("DeviceId is missing");
                return;
            }

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

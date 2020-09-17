using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShoppingCenter.DataLayer.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCenter.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateWebHostBuilder(args).Build();
			using (var serviceScope = host.Services.CreateScope())
			{
				var services = serviceScope.ServiceProvider;
				try
				{
					var serviceContext = services.GetRequiredService<IProductService>();
					if (serviceContext is FakeProductService)
					{
						((FakeProductService)serviceContext).InitializeDataAsync().Wait();
					}
				}
				catch (Exception ex)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred.");
				}
			}

			host.RunAsync().Wait();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}

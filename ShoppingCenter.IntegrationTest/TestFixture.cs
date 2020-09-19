using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShoppingCenter.Api;
using ShoppingCenter.DataLayer.Services;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace ShoppingCenter.IntegrationTest
{
	public class TestFixture<TStartup> : IDisposable
	{
		public static string GetProjectPath(string projectRelativePath, Assembly startupAssembly)
		{
			var projectName = startupAssembly.GetName().Name;

			var applicationBasePath = AppContext.BaseDirectory;

			var directoryInfo = new DirectoryInfo(applicationBasePath);

			do
			{
				directoryInfo = directoryInfo.Parent;

				var projectDirectoryInfo = new DirectoryInfo(Path.Combine(directoryInfo.FullName, projectRelativePath));

				if (projectDirectoryInfo.Exists)
				{
					if (new FileInfo(Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj")).Exists)
					{
						return Path.Combine(projectDirectoryInfo.FullName, projectName);
					}
				}
			}
			while (directoryInfo.Parent != null);

			throw new Exception($"Project root could not be located using the application root {applicationBasePath}.");
		}

		private TestServer Server;

		public TestFixture()
			: this(Path.Combine(""))
		{
		}

		public HttpClient Client { get; }

		public void Dispose()
		{
			Client.Dispose();
			Server.Dispose();
		}

		protected virtual void InitializeServices(IServiceCollection services)
		{
			var startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;

			var manager = new ApplicationPartManager
			{
				ApplicationParts =
				{
					new AssemblyPart(startupAssembly)
				},
				FeatureProviders =
				{
					new ControllerFeatureProvider(),
					new ViewComponentFeatureProvider()
				}
			};

			services.AddSingleton(manager);
		}

		protected TestFixture(string relativeTargetProjectParentDir)
		{
			var startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;
			var contentRoot = GetProjectPath(relativeTargetProjectParentDir, startupAssembly);

			var configurationBuilder = new ConfigurationBuilder()
				.SetBasePath(contentRoot)
				.AddJsonFile("appsettings.json");

			var webHostBuilder = new WebHostBuilder()
				.UseContentRoot(contentRoot)
				.ConfigureServices(InitializeServices)
				.UseConfiguration(configurationBuilder.Build())
				.UseEnvironment("Development")
				.UseStartup(typeof(TStartup));

			// Create instance of test server
			Server = new TestServer(webHostBuilder);
			InitializeData();

			// Add configuration for client
			Client = Server.CreateClient();
			Client.BaseAddress = new Uri("http://localhost:52500");
			Client.DefaultRequestHeaders.Accept.Clear();
			Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		private void InitializeData()
		{
			using (var serviceScope = Server.Host.Services.CreateScope())
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
		}
	}
}
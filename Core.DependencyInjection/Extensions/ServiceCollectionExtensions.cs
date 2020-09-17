using Core.DependencyInjection.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

namespace Core.DependencyInjection.Extensions
{
	public static class ServiceCollectionExtensions
	{

		/// <summary>
		/// Scans all assemblies and finds injected services.
		/// </summary>
		public static IServiceCollection InjectServiceAssembly(this IServiceCollection services, bool isDevelopment, bool isProduction)
		{
			var rootPath = Directory.GetParent(AppContext.BaseDirectory).FullName;
			var dllAddresses = Directory.GetFiles(rootPath, "*.dll", SearchOption.TopDirectoryOnly);
			foreach (var dll in dllAddresses)
			{
				try
				{
					ScanAssembly(services, dll, isDevelopment, isProduction);
				}
				catch (BadImageFormatException)
				{

				}
			}
			return services;
		}

		private static void ScanAssembly(IServiceCollection services, string assemblyPath, bool isDevelopment, bool isProduction)
		{

			var assembly = Assembly.LoadFrom(assemblyPath);

			foreach (var type in assembly.ExportedTypes)
			{
				var dependencyAttributes = type.GetCustomAttributes<DependencyAttribute>();
				foreach (var dependencyAttribute in dependencyAttributes)
				{
					if (dependencyAttribute.DevelopmentOnly == true && isProduction||
						dependencyAttribute.ProductionOnly == true && isDevelopment)
					{
						continue;
					}
					var serviceDescriptor = dependencyAttribute.BuildServiceDescriptor(type.GetTypeInfo());
					services.Add(serviceDescriptor);
				}
			}


			var currentAssembly = Assembly.GetExecutingAssembly();
			foreach (var type in currentAssembly.ExportedTypes)
			{
				var dependencyAttributes = type.GetCustomAttributes<DependencyAttribute>();
				foreach (var dependencyAttribute in dependencyAttributes)
				{
					var serviceDescriptor = dependencyAttribute.BuildServiceDescriptor(type.GetTypeInfo());
					services.Add(serviceDescriptor);
				}
			}
		}
	}
}

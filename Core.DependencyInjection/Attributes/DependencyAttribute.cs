using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Core.DependencyInjection.Attributes
{
	public class DependencyAttribute : Attribute
	{
		public ServiceLifetime DependencyType { get; set; }

		public Type ServiceType { get; set; }

		public Type[] GenericTypes { get; set; }

		protected DependencyAttribute(ServiceLifetime dependencyType, params Type[] genericTypes)
		{
			DependencyType = dependencyType;
			GenericTypes = genericTypes;
		}

		public ServiceDescriptor BuildServiceDescriptor(TypeInfo type)
		{
			var serviceType = ServiceType ?? type.AsType();
			return new ServiceDescriptor(serviceType, type.AsType(), DependencyType);
		}
	}
}

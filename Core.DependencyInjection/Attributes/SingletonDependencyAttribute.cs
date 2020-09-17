using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyInjection.Attributes
{
	public class SingletonDependencyAttribute : DependencyAttribute
	{
		public SingletonDependencyAttribute()
			: base(ServiceLifetime.Singleton)
		{
		}
	}
}

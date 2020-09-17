using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyInjection.Attributes
{
	public class ScopedDependencyAttribute : DependencyAttribute
	{
		public ScopedDependencyAttribute()
			: base(ServiceLifetime.Scoped)
		{
		}
	}
}

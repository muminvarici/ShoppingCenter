using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyInjection.Attributes
{
	public class TransientDependencyAttribute : DependencyAttribute
	{
		public TransientDependencyAttribute()
			: base(ServiceLifetime.Transient)
		{
		}
	}
}

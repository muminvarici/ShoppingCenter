using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DependencyInjection.Exceptions
{
	public class ServiceException : Exception
	{
		public ServiceException(string message) : base(message)
		{

		}
	}
}

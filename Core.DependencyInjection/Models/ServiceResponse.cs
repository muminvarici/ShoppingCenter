using System;

namespace Core.DependencyInjection.Models
{
	public class ServiceResponse
	{
		public ServiceResponse()
		{
			Succeeded = true;
		}

		public ServiceResponse(object result)
		{
			Succeeded = true;
			Result = result;
		}

		public bool Succeeded { get; set; }

		public string Message { get; set; }

		public Guid Ticket { get; set; }

		public object Result { get; set; }
	}
}

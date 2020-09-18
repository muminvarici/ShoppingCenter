using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.AppLayer.Commands.Carts
{
	public class DeleteCartCommand : IRequest
	{
		public string Id { get; set; }
	}
}

using CodeShopWarehouse.Business;
using CodeShopWarehouse.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CodeShopWarehouse.Web.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class FillOrdersController : Controller
	{

		private readonly FillOrderService _fillOrderService;

		public FillOrdersController(FillOrderService fillOrderService)
		{
			_fillOrderService = fillOrderService;
		}

		[HttpGet]
		public IActionResult GetFillOrders()
		{
			return Ok(_fillOrderService.GetFillOrders());
		}

		[HttpGet("{id}")]
		public IActionResult GetFillOrderById(int id)
		{
			return Ok(_fillOrderService.GetFillOrderById(id));
		}

		[HttpPost("process")]
		public IActionResult ProcessFillOrder([FromBody] FillOrder fillOrder)
		{
			if (!ModelState.IsValid)
			{
				throw new Exception();
			}
			_fillOrderService.ProcessFillOrder(fillOrder);
			return Ok();
		}

	}
}


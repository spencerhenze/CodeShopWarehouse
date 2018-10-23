using CodeShopWarehouse.Business;
using CodeShopWarehouse.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;

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
		[AllowAnonymous]
		[HttpGet]
		public IActionResult GetFillOrders()
		{
			return Ok(_fillOrderService.GetAllFillOrders());
		}

		[HttpGet("{id}")]
		public IActionResult GetFillOrderById(int id)
		{
			return Ok(_fillOrderService.GetFillOrderById(id));
		}

		[HttpPost]
		public IActionResult CreateNewOrder([FromBody] FillOrder fillOrder)
		{
			if (!ModelState.IsValid)
			{
				throw new Exception();
			}

			return Ok(_fillOrderService.CreateNewOrder(fillOrder));
		}

		[HttpPost("process")]
		public IActionResult ProcessFillOrder([FromBody] FillOrder fillOrder)
		{
			if (!ModelState.IsValid)
			{
				throw new Exception();
			}
			
			return Ok(_fillOrderService.ProcessFillOrder(fillOrder));
		}

	}
}


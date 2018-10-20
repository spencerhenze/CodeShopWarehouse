using CodeShopWarehouse.Data;
using System;
using System.Collections.Generic;
using CodeShopWarehouse.Entities;

namespace CodeShopWarehouse.Business
{
	public class FillOrderService
	{
		private readonly IFillOrderRepo _fillOrderRepo;

		public FillOrderService(IFillOrderRepo fillOrderRepo)
		{
			_fillOrderRepo = fillOrderRepo;
		}

		public List<FillOrder> GetAllFillOrders()
		{
			return _fillOrderRepo.GetAllFillOrders();
		}

		public List<FillOrder> GetUnresolvedFillOrders()
		{
			return _fillOrderRepo.GetUnresolvedFillOrders();
		}

		public List<FillOrder> GetFillOrdersByProductId(int productId)
		{
			return _fillOrderRepo.GetOrdersByProductId(productId);
		}

		public FillOrder GetFillOrderById(int id)
		{
			return _fillOrderRepo.GetFillOrderById(id);
		}

		public FillOrder ProcessFillOrder(FillOrder fillOrder)
		{
			var currentFillOrder = GetFillOrderById(fillOrder.Id);

			if (currentFillOrder == null) { throw new Exception("Fill Order not found"); }

			if (currentFillOrder.ProcessedTimestamp != null)
			{
				throw new Exception("Fill Order has already been processed");
			}

			currentFillOrder.ProcessedTimestamp = DateTimeOffset.Now;

			_fillOrderRepo.UpdateFillOrder(currentFillOrder);

			return currentFillOrder;
		}

	}
}

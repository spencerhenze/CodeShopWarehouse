using System;
using System.Collections.Generic;
using System.Data;
using CodeShopWarehouse.Entities;

namespace CodeShopWarehouse.Data
{
	public interface IFillOrderRepo
	{
		FillOrder GetFillOrderById(int id);
		List<FillOrder> GetFillOrders();
		void UpdateFillOrder(FillOrder fillOrder);
	}

	public class FillOrderRepo: IFillOrderRepo
	{
		private readonly IDbConnection _db;

		private List<FillOrder> _fillOrders = new List<FillOrder>();

		private void BuildFillOrders()
		{
			Random randomGen = new Random();
			for (int i = 5; i > 0; i--)
			{
				var fillOrder = new FillOrder()
				{
					Id = i,
					OrderType = i % 2 == 0 ? OrderTypeEnum.AddStock : OrderTypeEnum.RemoveStock,
					ProductId = randomGen.Next(100, 500),
					Quantity = i * randomGen.Next(5, 100)
				};
				_fillOrders.Add(fillOrder);
			}

		}

		public FillOrderRepo(IDbConnection db)
		{
			_db = db;
			BuildFillOrders();
		}


		public FillOrder GetFillOrderById(int id)
		{
			return _fillOrders.Find(x => x.Id == id);
		}

		public List<FillOrder> GetFillOrders()
		{
			return _fillOrders;
		}

		public void UpdateFillOrder(FillOrder fillOrder)
		{
			var fillOrderToUpdate = _fillOrders.Find(x => x.Id == fillOrder.Id);

			if (fillOrderToUpdate == null)
			{
				throw new Exception("Fill order does not exist");
			}

			fillOrderToUpdate = fillOrder;
		}
	}
}

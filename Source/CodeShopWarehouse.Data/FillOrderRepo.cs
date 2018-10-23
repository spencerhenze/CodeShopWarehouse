using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CodeShopWarehouse.Entities;

namespace CodeShopWarehouse.Data
{
	public interface IFillOrderRepo
	{
		FillOrder GetFillOrderById(int id);
		List<FillOrder> GetOrdersByProductId(int productId);
		List<FillOrder> GetAllFillOrders();
		List<FillOrder> GetUnresolvedFillOrders();
		void UpdateFillOrder(FillOrder fillOrder);
		string CreateNewOrder(FillOrder fillOrder);
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


		public List<FillOrder> GetAllFillOrders()
		{
			return _fillOrders;
		}

		public List<FillOrder> GetUnresolvedFillOrders()
		{
			return _fillOrders.Where(x => x.ProcessedTimestamp == null).ToList();
		}


		public void UpdateFillOrder(FillOrder fillOrder)
		{
			var fillOrderToUpdate = GetFillOrderById(fillOrder.Id);

			if (fillOrderToUpdate == null)
			{
				throw new Exception("Fill order does not exist");
			}

			_fillOrders.Remove(fillOrderToUpdate);
			_fillOrders.Add(fillOrder);
		}

		public List<FillOrder> GetOrdersByProductId(int productId)
		{
			return _fillOrders.Where(x => x.ProductId == productId).ToList();
		}

		public string CreateNewOrder(FillOrder order)
		{
			return "Done";
		}
	}
}

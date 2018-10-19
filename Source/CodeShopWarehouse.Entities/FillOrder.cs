using System;

namespace CodeShopWarehouse.Entities
{
	public class FillOrder
	{
		public int Id { get; set; }
		public DateTimeOffset? ProcessedTimestamp { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public OrderTypeEnum OrderType { get; set; }
	}
}

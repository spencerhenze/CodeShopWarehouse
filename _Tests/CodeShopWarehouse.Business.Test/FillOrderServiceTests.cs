using System;
using System.Collections.Generic;
using CodeShopWarehouse.Data;
using CodeShopWarehouse.Entities;
using Xunit;
using NSubstitute;
using NSubstitute.Extensions;

namespace CodeShopWarehouse.Business.Test
{
	public class FillOrderServiceTests
	{
		private IFillOrderRepo _mockFillOrderRepo;
		private FillOrderService _fillOrderService;
		private List<FillOrder> unresolvedFillOrders;

		public FillOrderServiceTests()
		{
			_mockFillOrderRepo = Substitute.For<IFillOrderRepo>();
			_fillOrderService = new FillOrderService(_mockFillOrderRepo);
			unresolvedFillOrders = new List<FillOrder>();

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
				unresolvedFillOrders.Add(fillOrder);
			}

		}

		[Fact]
		public void Unresolved_FillOrders_CanBeRetrieved()
		{
			// Arrange

			_mockFillOrderRepo.GetUnresolvedFillOrders().Returns(unresolvedFillOrders);

			// Act
			var actualFillOrders = _fillOrderService.GetUnresolvedFillOrders();

			// Assert
			Assert.Equal(5, actualFillOrders.Count);
		}

		[Fact]
		public void Unresolved_FillOrders_Can_Be_Processed()
		{
			// Arrange
			var fillOrderToBeProcessed = new FillOrder()
			{
				Id = 1121,
			};

			_mockFillOrderRepo.GetFillOrderById(fillOrderToBeProcessed.Id).Returns(new FillOrder()
			{
				Id = 1121,
				ProcessedTimestamp = null
			});

			// Act
			var processedFillOrder = _fillOrderService.ProcessFillOrder(fillOrderToBeProcessed);

			// Assert
			Assert.NotNull(processedFillOrder.ProcessedTimestamp);
			_mockFillOrderRepo.Received(1).GetFillOrderById(fillOrderToBeProcessed.Id);
			_mockFillOrderRepo.ReceivedWithAnyArgs(1).UpdateFillOrder(processedFillOrder);
		}

		[Fact]
		public void Resolved_FillOrders_Can_Not_Be_Processed()
		{
			// Arrange
			var fillOrderToBeProcessed = new FillOrder()
			{
				Id = 1121,
				ProcessedTimestamp = DateTimeOffset.Now
			};

			_mockFillOrderRepo.GetFillOrderById(fillOrderToBeProcessed.Id).Returns(new FillOrder()
			{
				Id = 1121,
				ProcessedTimestamp = DateTimeOffset.Now.AddDays(-1)
			});

			// Act
			try
			{
				var result = _fillOrderService.ProcessFillOrder(fillOrderToBeProcessed);
			}
			catch (Exception ex)
			{
				// Assert
				Assert.Equal("Fill Order has already been processed", ex.Message);
			}

		}
	}
}

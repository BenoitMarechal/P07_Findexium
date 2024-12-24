using Microsoft.VisualStudio.TestTools.UnitTesting;
using P07_Findexium_Unit_Tests;
using System;
using System.Collections.Generic;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services;
using Moq;
using P7CreateRestApi.Repositories;
using Dot.Net.WebApi.Domain;




namespace P07_Findexium_Unit_Tests
{
  
    [TestClass]
    public class TradeRepositoryUnitTests
    {
        [TestMethod]

        public async Task AddTrade_ShouldCallRepositoryAdd()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Trade>>();

            // Configure the repository's Add method to return Task.CompletedTask (as it's void in your service)
            mockRepository
                .Setup(r => r.Add(It.IsAny<Trade>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Initialize the service with the mocked repository
            var tradeService = new TradeService(mockRepository.Object);
            var trade = new Trade { TradeId = 10 };

            // Test input

            // Act
            //  await tradeService.Add(trade);
             await tradeService.Add(trade);

            // Assert
            mockRepository.Verify(r => r.Add(It.Is<Trade>(e => e.TradeId == 10)), Times.Once);
        }

        [TestMethod]

        public async Task GetTrades_ShouldReturn_AllTrades()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Trade>>();

            var tradesList = new List<Trade>()
            {
                new Trade { DealName="string" },
                new Trade { DealName="string"},
            };

            mockRepository
                .Setup(r => r.GetAll())
                .ReturnsAsync(tradesList)
                .Verifiable();

            // Initialize the service with the mocked repository
            var tradeService = new TradeService(mockRepository.Object);


            // Act
            // Get all Trades
            var result = await tradeService.GetAll(); 

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            mockRepository.Verify(r => r.GetAll(), Times.Once);
        }


        [TestMethod]

        public async Task GetTradeById_ShouldReturn_Trade()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Trade>>();

            var tradesList = new List<Trade>()
            {
                new Trade { DealName = "string", TradeId=1 },
                new Trade { DealName = "string", TradeId=2  },
            };

            mockRepository
                .Setup(r => r.GetById(It.IsAny<int>()))
                .ReturnsAsync(tradesList.FirstOrDefault())
                .Verifiable()
                ; 
       

            // Initialize the service with the mocked repository
            var tradeService = new TradeService(mockRepository.Object);

            // Act          
            var result = await tradeService.GetById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.TradeId);
            mockRepository.Verify(r => r.GetById(1), Times.Once);
        }

        [TestMethod]

        public async Task UpdateTrade_ShouldCallUpdate()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Trade>>();

            var trade = new Trade { DealName = "string", TradeId = 1 };

            mockRepository
        .Setup(r => r.GetById(trade.TradeId))
        .ReturnsAsync(trade)
        .Verifiable();

            mockRepository
         .Setup(r => r.Update(It.IsAny<Trade>()))
         .Returns(Task.CompletedTask) // Use this if no meaningful return value is needed
         .Verifiable();



            // Initialize the service with the mocked repository
            var tradeService = new TradeService(mockRepository.Object);

            // Act          
             await tradeService.Update(trade);

         
            mockRepository.Verify(r => r.Update(trade), Times.Once);
        }
        [TestMethod]

        public async Task DeleteTrade_ShouldCallDelete()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Trade>>();

            var trade = new Trade { DealName = "string", TradeId= 1 };

            mockRepository
        .Setup(r => r.GetById(trade.TradeId))
        .ReturnsAsync(trade)
        .Verifiable();

            mockRepository
         .Setup(r => r.Delete(It.IsAny<int>()))
         .Returns(Task.CompletedTask) // Use this if no meaningful return value is needed
         .Verifiable();



            // Initialize the service with the mocked repository
            var tradeService = new TradeService(mockRepository.Object);

            // Act          
            await tradeService.Delete(trade.TradeId);


            mockRepository.Verify(r => r.Delete(trade.TradeId), Times.Once);
        }


    }




}



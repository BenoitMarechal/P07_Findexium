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
    public class BidListRepositoryUnitTests
    {
        [TestMethod]

        public async Task AddBidList_ShouldCallRepositoryAdd()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<BidList>>();

            // Configure the repository's Add method to return Task.CompletedTask (as it's void in your service)
            mockRepository
                .Setup(r => r.Add(It.IsAny<BidList>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Initialize the service with the mocked repository
            var bidListService = new BidListService(mockRepository.Object);
            var bidList = new BidList { BidListId = 10 };

            // Test input

            // Act
            //  await bidListService.Add(bidList);
             await bidListService.Add(bidList);

            // Assert
            mockRepository.Verify(r => r.Add(It.Is<BidList>(e => e.BidListId == 10)), Times.Once);
        }

        [TestMethod]

        public async Task GetBidLists_ShouldReturn_AllBidLists()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<BidList>>();

            var bidListsList = new List<BidList>()
            {
                new BidList { BidStatus="string" },
                new BidList { BidStatus="string"},
            };

            mockRepository
                .Setup(r => r.GetAll())
                .ReturnsAsync(bidListsList)
                .Verifiable();

            // Initialize the service with the mocked repository
            var bidListService = new BidListService(mockRepository.Object);


            // Act
            // Get all BidLists
            var result = await bidListService.GetAll(); 

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            mockRepository.Verify(r => r.GetAll(), Times.Once);
        }


        [TestMethod]

        public async Task GetBidListById_ShouldReturn_BidList()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<BidList>>();

            var bidListsList = new List<BidList>()
            {
                new BidList { BidStatus = "string", BidListId=1 },
                new BidList { BidStatus = "string", BidListId=2  },
            };

            mockRepository
                .Setup(r => r.GetById(It.IsAny<int>()))
                .ReturnsAsync(bidListsList.FirstOrDefault())
                .Verifiable()
                ; 
       

            // Initialize the service with the mocked repository
            var bidListService = new BidListService(mockRepository.Object);

            // Act          
            var result = await bidListService.GetById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.BidListId);
            mockRepository.Verify(r => r.GetById(1), Times.Once);
        }

        [TestMethod]

        public async Task UpdateBidList_ShouldCallUpdate()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<BidList>>();

            var bidList = new BidList { BidStatus = "string", BidListId = 1 };

            mockRepository
        .Setup(r => r.GetById(bidList.BidListId))
        .ReturnsAsync(bidList)
        .Verifiable();

            mockRepository
         .Setup(r => r.Update(It.IsAny<BidList>()))
         .Returns(Task.CompletedTask) // Use this if no meaningful return value is needed
         .Verifiable();



            // Initialize the service with the mocked repository
            var bidListService = new BidListService(mockRepository.Object);

            // Act          
             await bidListService.Update(bidList);

         
            mockRepository.Verify(r => r.Update(bidList), Times.Once);
        }
        [TestMethod]

        public async Task DeleteBidList_ShouldCallDelete()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<BidList>>();

            var bidList = new BidList { BidStatus = "string", BidListId= 1 };

            mockRepository
        .Setup(r => r.GetById(bidList.BidListId))
        .ReturnsAsync(bidList)
        .Verifiable();

            mockRepository
         .Setup(r => r.Delete(It.IsAny<int>()))
         .Returns(Task.CompletedTask) // Use this if no meaningful return value is needed
         .Verifiable();



            // Initialize the service with the mocked repository
            var bidListService = new BidListService(mockRepository.Object);

            // Act          
            await bidListService.Delete(bidList.BidListId);


            mockRepository.Verify(r => r.Delete(bidList.BidListId), Times.Once);
        }


    }




}



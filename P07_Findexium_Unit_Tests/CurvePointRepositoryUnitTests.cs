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
    public class CurvePointRepositoryUnitTests
    {
        [TestMethod]

        public async Task AddCurvePoint_ShouldCallRepositoryAdd()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<CurvePoint>>();

            // Configure the repository's Add method to return Task.CompletedTask (as it's void in your service)
            mockRepository
                .Setup(r => r.Add(It.IsAny<CurvePoint>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Initialize the service with the mocked repository
            var curvePointService = new CurvePointService(mockRepository.Object);
            var curvePoint = new CurvePoint { Id = 10 };

            // Test input

            // Act
            //  await curvePointService.Add(curvePoint);
             await curvePointService.Add(curvePoint);

            // Assert
            mockRepository.Verify(r => r.Add(It.Is<CurvePoint>(cp => cp.Id == 10)), Times.Once);
        }

        [TestMethod]

        public async Task GetCurvePoints_ShouldReturn_AllCurvePoints()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<CurvePoint>>();

            var curvePointsList = new List<CurvePoint>()
            {
                new CurvePoint { CurvePointValue=20.5 },
                new CurvePoint { CurvePointValue=10.5 },
            };

            mockRepository
                .Setup(r => r.GetAll())
                .ReturnsAsync(curvePointsList)
                .Verifiable();

            // Initialize the service with the mocked repository
            var curvePointService = new CurvePointService(mockRepository.Object);


            // Act
            // Get all CurvePoints
            var result = await curvePointService.GetAll(); 

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            mockRepository.Verify(r => r.GetAll(), Times.Once);
        }


        [TestMethod]

        public async Task GetCurvePointById_ShouldReturn_CurvePoint()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<CurvePoint>>();

            var curvePointsList = new List<CurvePoint>()
            {
                new CurvePoint { CurvePointValue=20.5, Id=1 },
                new CurvePoint { CurvePointValue=10.5, Id=2  },
            };

            mockRepository
                .Setup(r => r.GetById(It.IsAny<int>()))
                .ReturnsAsync(curvePointsList.FirstOrDefault())
                .Verifiable()
                ; 
       

            // Initialize the service with the mocked repository
            var curvePointService = new CurvePointService(mockRepository.Object);

            // Act          
            var result = await curvePointService.GetById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            mockRepository.Verify(r => r.GetById(1), Times.Once);
        }

        [TestMethod]

        public async Task UpdateCurvePoint_ShouldCallUpdate()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<CurvePoint>>();

            var curvePoint = new CurvePoint { CurvePointValue = 20.5, Id = 1 };

            mockRepository
        .Setup(r => r.GetById(curvePoint.Id))
        .ReturnsAsync(curvePoint)
        .Verifiable();

            mockRepository
         .Setup(r => r.Update(It.IsAny<CurvePoint>()))
         .Returns(Task.CompletedTask) // Use this if no meaningful return value is needed
         .Verifiable();



            // Initialize the service with the mocked repository
            var curvePointService = new CurvePointService(mockRepository.Object);

            // Act          
             await curvePointService.Update(curvePoint);

         
            mockRepository.Verify(r => r.Update(curvePoint), Times.Once);
        }
        [TestMethod]

        public async Task DeleteCurvePoint_ShouldCallDelete()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<CurvePoint>>();

            var curvePoint = new CurvePoint { CurvePointValue = 20.5, Id = 1 };

            mockRepository
        .Setup(r => r.GetById(curvePoint.Id))
        .ReturnsAsync(curvePoint)
        .Verifiable();

            mockRepository
         .Setup(r => r.Delete(It.IsAny<int>()))
         .Returns(Task.CompletedTask) // Use this if no meaningful return value is needed
         .Verifiable();



            // Initialize the service with the mocked repository
            var curvePointService = new CurvePointService(mockRepository.Object);

            // Act          
            await curvePointService.Delete(curvePoint.Id);


            mockRepository.Verify(r => r.Delete(curvePoint.Id), Times.Once);
        }


    }




}



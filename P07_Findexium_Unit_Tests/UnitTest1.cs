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
    public class UnitTest1
    {
        [TestMethod]

        public async Task AddCurvePoint_ShouldCallRepositoryAdd()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<CurvePoint>>();

            // Configure the repository's Add method to return Task.CompletedTask (as it's void in your service)
            mockRepository
                .Setup(r => r.Add(It.IsAny<CurvePoint>()))
                .Returns(Task.FromResult(1))
                .Verifiable();

            // Initialize the service with the mocked repository
            var curvePointService = new CurvePointService(mockRepository.Object);

            // Test input
            var curvePoint = new CurvePoint { Id = 10 };

            // Act
           //  await curvePointService.Add(curvePoint);
           // var result = await curvePointService.Add(curvePoint);

            // Assert


          //  Assert.AreEqual(result, 1);
        }
        //public void GetAllCurvePoints_ShouldReturnAllCurvePoints()
        //{
        //    var mockRepository = new Mock<IRepository<CurvePoint>>();
        //    var curvePointService = new Mock<CurvePointService>(mockRepository);
        //    var curvePoint = new CurvePoint { Id = "1"};
        //    mockRepository
        //         .Setup(r => r.Add(It.IsAny<CurvePoint>())).Returns(It.IsAny<Task>);

        //    curvePointService
        //         .Setup(r => r.Add(It.IsAny<CurvePoint>()));

        //    var result = curvePointService.Object.Add(curvePoint);

        //    Assert.IsNotNull(result);    

        //}

        private object GetTestCurvePoints()
        {
            throw new NotImplementedException();
        }
    }

  
}



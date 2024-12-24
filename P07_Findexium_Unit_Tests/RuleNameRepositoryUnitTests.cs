using Microsoft.VisualStudio.TestTools.UnitTesting;
using P07_Findexium_Unit_Tests;
using System;
using System.Collections.Generic;
using P7CreateRestApi.Models;
using P7CreateRestApi.Services;
using Moq;
using P7CreateRestApi.Repositories;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Controllers.Domain;
using Dot.Net.WebApi.Controllers;




namespace P07_Findexium_Unit_Tests
{
  
    [TestClass]
    public class RuleNameRepositoryUnitTests
    {
        [TestMethod]

        public async Task AddRuleName_ShouldCallRepositoryAdd()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<RuleName>>();

            // Configure the repository's Add method to return Task.CompletedTask (as it's void in your service)
            mockRepository
                .Setup(r => r.Add(It.IsAny<RuleName>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Initialize the service with the mocked repository
            var ruleNameService = new RuleNameService(mockRepository.Object);
            var ruleName = new RuleName { Id = 10 };

            // Test input

            // Act
            //  await ruleNameService.Add(ruleName);
             await ruleNameService.Add(ruleName);

            // Assert
            mockRepository.Verify(r => r.Add(It.Is<RuleName>(cp => cp.Id == 10)), Times.Once);
        }

        [TestMethod]

        public async Task GetRuleNames_ShouldReturn_AllRuleNames()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<RuleName>>();

            var ruleNamesList = new List<RuleName>()
            {
                new RuleName { Name="" },
                new RuleName { Name=""},
            };

            mockRepository
                .Setup(r => r.GetAll())
                .ReturnsAsync(ruleNamesList)
                .Verifiable();

            // Initialize the service with the mocked repository
            var ruleNameService = new RuleNameService(mockRepository.Object);


            // Act
            // Get all RuleNames
            var result = await ruleNameService.GetAll(); 

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            mockRepository.Verify(r => r.GetAll(), Times.Once);
        }


        [TestMethod]

        public async Task GetRuleNameById_ShouldReturn_RuleName()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<RuleName>>();

            var ruleNamesList = new List<RuleName>()
            {
                new RuleName { Name="", Id=1 },
                new RuleName { Name="", Id=2  },
            };

            mockRepository
                .Setup(r => r.GetById(It.IsAny<int>()))
                .ReturnsAsync(ruleNamesList.FirstOrDefault())
                .Verifiable()
                ; 
       

            // Initialize the service with the mocked repository
            var ruleNameService = new RuleNameService(mockRepository.Object);

            // Act          
            var result = await ruleNameService.GetById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            mockRepository.Verify(r => r.GetById(1), Times.Once);
        }

        [TestMethod]

        public async Task UpdateRuleName_ShouldCallUpdate()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<RuleName>>();

            var ruleName = new RuleName { Name = "", Id = 1 };

            mockRepository
        .Setup(r => r.GetById(ruleName.Id))
        .ReturnsAsync(ruleName)
        .Verifiable();

            mockRepository
         .Setup(r => r.Update(It.IsAny<RuleName>()))
         .Returns(Task.CompletedTask) // Use this if no meaningful return value is needed
         .Verifiable();



            // Initialize the service with the mocked repository
            var ruleNameService = new RuleNameService(mockRepository.Object);

            // Act          
             await ruleNameService.Update(ruleName);

         
            mockRepository.Verify(r => r.Update(ruleName), Times.Once);
        }
        [TestMethod]

        public async Task DeleteRuleName_ShouldCallDelete()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<RuleName>>();

            var ruleName = new RuleName { Name = "", Id = 1 };

            mockRepository
        .Setup(r => r.GetById(ruleName.Id))
        .ReturnsAsync(ruleName)
        .Verifiable();

            mockRepository
         .Setup(r => r.Delete(It.IsAny<int>()))
         .Returns(Task.CompletedTask) // Use this if no meaningful return value is needed
         .Verifiable();



            // Initialize the service with the mocked repository
            var ruleNameService = new RuleNameService(mockRepository.Object);

            // Act          
            await ruleNameService.Delete(ruleName.Id);


            mockRepository.Verify(r => r.Delete(ruleName.Id), Times.Once);
        }


    }




}



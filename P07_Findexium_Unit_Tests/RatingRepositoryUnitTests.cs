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




namespace P07_Findexium_Unit_Tests
{
  
    [TestClass]
    public class RatingRepositoryUnitTests
    {
        [TestMethod]

        public async Task AddRating_ShouldCallRepositoryAdd()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Rating>>();

            // Configure the repository's Add method to return Task.CompletedTask (as it's void in your service)
            mockRepository
                .Setup(r => r.Add(It.IsAny<Rating>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Initialize the service with the mocked repository
            var ratingService = new RatingService(mockRepository.Object);
            var rating = new Rating { Id = 10 };

            // Test input

            // Act
            //  await ratingService.Add(rating);
             await ratingService.Add(rating);

            // Assert
            mockRepository.Verify(r => r.Add(It.Is<Rating>(cp => cp.Id == 10)), Times.Once);
        }

        [TestMethod]

        public async Task GetRatings_ShouldReturn_AllRatings()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Rating>>();

            var ratingsList = new List<Rating>()
            {
                new Rating { MoodysRating="" },
                new Rating { MoodysRating=""},
            };

            mockRepository
                .Setup(r => r.GetAll())
                .ReturnsAsync(ratingsList)
                .Verifiable();

            // Initialize the service with the mocked repository
            var ratingService = new RatingService(mockRepository.Object);


            // Act
            // Get all Ratings
            var result = await ratingService.GetAll(); 

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            mockRepository.Verify(r => r.GetAll(), Times.Once);
        }


        [TestMethod]

        public async Task GetRatingById_ShouldReturn_Rating()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Rating>>();

            var ratingsList = new List<Rating>()
            {
                new Rating { MoodysRating="", Id=1 },
                new Rating { MoodysRating="", Id=2  },
            };

            mockRepository
                .Setup(r => r.GetById(It.IsAny<int>()))
                .ReturnsAsync(ratingsList.FirstOrDefault())
                .Verifiable()
                ; 
       

            // Initialize the service with the mocked repository
            var ratingService = new RatingService(mockRepository.Object);

            // Act          
            var result = await ratingService.GetById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            mockRepository.Verify(r => r.GetById(1), Times.Once);
        }

        [TestMethod]

        public async Task UpdateRating_ShouldCallUpdate()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Rating>>();

            var rating = new Rating { MoodysRating = "", Id = 1 };

            mockRepository
        .Setup(r => r.GetById(rating.Id))
        .ReturnsAsync(rating)
        .Verifiable();

            mockRepository
         .Setup(r => r.Update(It.IsAny<Rating>()))
         .Returns(Task.CompletedTask) // Use this if no meaningful return value is needed
         .Verifiable();



            // Initialize the service with the mocked repository
            var ratingService = new RatingService(mockRepository.Object);

            // Act          
             await ratingService.Update(rating);

         
            mockRepository.Verify(r => r.Update(rating), Times.Once);
        }
        [TestMethod]

        public async Task DeleteRating_ShouldCallDelete()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Rating>>();

            var rating = new Rating { MoodysRating = "", Id = 1 };

            mockRepository
        .Setup(r => r.GetById(rating.Id))
        .ReturnsAsync(rating)
        .Verifiable();

            mockRepository
         .Setup(r => r.Delete(It.IsAny<int>()))
         .Returns(Task.CompletedTask) // Use this if no meaningful return value is needed
         .Verifiable();



            // Initialize the service with the mocked repository
            var ratingService = new RatingService(mockRepository.Object);

            // Act          
            await ratingService.Delete(rating.Id);


            mockRepository.Verify(r => r.Delete(rating.Id), Times.Once);
        }


    }




}



using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Repositories;
using Dot.Net.WebApi.Controllers.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace P7CreateRestApi.Services
{
    public class RatingService
    {
        private readonly IRepository<Rating> _ratingRepository;

        public RatingService(IRepository<Rating> ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        // Adds a new Rating entity to the repository
        public async Task Add(Rating rating)
        {
            await _ratingRepository.Add(rating);
        }

        // Retrieves all Rating entities from the repository
        public async Task<IEnumerable<Rating>> GetAll()
        {
            return await _ratingRepository.GetAll();
        }

        // Retrieves a Rating by its ID, throws KeyNotFoundException if not found
        public async Task<Rating> GetById(int id)
        {
            var result = await _ratingRepository.GetById(id);
            if (result == null)
            {
                throw new KeyNotFoundException($"Rating with ID {id} not found.");
            }
            return result;
        }

        // Updates a Rating entity, throws KeyNotFoundException if not found
        public async Task Update(Rating rating)
        {
            var existingRating = await _ratingRepository.GetById(rating.Id);
            if (existingRating == null)
            {
                throw new KeyNotFoundException($"Rating with ID {rating.Id} not found.");
            }
            await _ratingRepository.Update(rating);
        }

        // Deletes a Rating by its ID, throws KeyNotFoundException if not found
        public async Task Delete(int id)
        {
            var existingRating = await _ratingRepository.GetById(id);
            if (existingRating == null)
            {
                throw new KeyNotFoundException($"Rating with ID {id} not found.");
            }
            await _ratingRepository.Delete(id);
        }

        // Checks if a Rating exists in the repository by ID
        public async Task<bool> RatingExists(int id)
        {
            return await _ratingRepository.GetById(id) != null;
        }
    }
}

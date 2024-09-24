using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Repositories;
using Dot.Net.WebApi.Controllers.Domain;

namespace P7CreateRestApi.Services
{
    public class RatingService
    {
        private readonly IRepository<Rating> _ratingRepository;

        public RatingService(IRepository<Rating> ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public Task Add(Rating rating)
        {
            return _ratingRepository.Add(rating);
        }

        public Task Update(Rating rating)
        {
            return _ratingRepository.Update(rating);
        }
        public Task Delete(int id)
        {
                return _ratingRepository.Delete(id);
        }
        
        public  Task<Rating> GetById(int id)
        {
             return  _ratingRepository.GetById(id);  
        }
        public Task<IEnumerable<Rating>> GetAll()
        {
            return _ratingRepository.GetAll();
        }
   
    }
}

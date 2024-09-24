using Dot.Net.WebApi.Controllers.Domain;
using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories
{
    public class RatingRepository : IRepository<Rating>
    {
        private readonly LocalDbContext _context;
        private readonly ILogger<RatingRepository> _logger;

        public RatingRepository(LocalDbContext context, ILogger<RatingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Add(Rating entity)
        {
            try
            {
                _context.Ratings.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while adding the rating.");
                throw new Exception("An error occurred while adding the rating.", ex);
            }
        }



        public async Task<IEnumerable<Rating>> GetAll()
        {
            try
            {
                return await _context.Ratings.ToListAsync();

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while adding the rating.");
                throw new Exception("An error occurred while adding the rating.", ex);
            }

        }

        public async Task<Rating> GetById(int id)
        {
            var result = await _context.Ratings.FirstOrDefaultAsync(r => r.Id == id);
            if (result == null)
            {
                _logger.LogError("Not found");
            }
            return result!;

        }

        public async Task Update(Rating entity)
        {
            var rating = await _context.Ratings.FindAsync(entity.Id);
            if (rating == null)
            {
                _logger.LogError($"Rating with ID {entity.Id} not found.");
                throw new KeyNotFoundException($"Rating with ID {entity.Id} not found.");
            }
            else

            _context.Entry(rating).State = EntityState.Detached;
            _context.Ratings.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {

            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                _logger.LogError($"Rating with ID {id} not found.");
                throw new KeyNotFoundException($"Rating with ID {id} not found.");
            }


            try
            {
                _context.Ratings.Remove(rating);
                await _context.SaveChangesAsync();

            }


            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while adding the rating.");
                throw new Exception("An error occurred while adding the rating.", ex);
            }

        }
    }
}

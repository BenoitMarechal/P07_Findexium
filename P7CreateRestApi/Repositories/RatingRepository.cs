using Dot.Net.WebApi.Controllers.Domain;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
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
            _context.Ratings.Add(entity);
            await _context.SaveChangesAsync();
        }



        public async Task<IEnumerable<Rating>> GetAll()
        {
            return await _context.Ratings.ToListAsync();

        }

        public async Task<Rating> GetById(int id)
        {
            return await _context.Ratings.FirstOrDefaultAsync(r => r.Id == id);

        }

        public async Task Update(Rating entity)
        {
            var Rating = await _context.Ratings.FindAsync(entity.Id);

            _context.Entry(Rating).State = EntityState.Detached;
            _context.Ratings.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {

            var Rating = await _context.Ratings.FindAsync(id);
            _context.Ratings.Remove(Rating);
            await _context.SaveChangesAsync();


        }
    }
}

using Dot.Net.WebApi.Controllers.Domain;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories
{
    public class TradeRepository : IRepository<Trade>
    {
        private readonly LocalDbContext _context;
        private readonly ILogger<TradeRepository> _logger;


        public TradeRepository(LocalDbContext context, ILogger<TradeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Add(Trade entity)
        {
            _context.Trades.Add(entity);
            await _context.SaveChangesAsync();
        }



        public async Task<IEnumerable<Trade>> GetAll()
        {
            return await _context.Trades.ToListAsync();

        }

        public async Task<Trade> GetById(string id)
        {
            return await _context.Trades.FirstOrDefaultAsync(r => r.TradeId == id);

        }

        public async Task Update(Trade entity)
        {
            var Trade = await _context.Trades.FindAsync(entity.TradeId);

            _context.Entry(Trade).State = EntityState.Detached;
            _context.Trades.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {

            var Trade = await _context.Trades.FindAsync(id);
            _context.Trades.Remove(Trade);
            await _context.SaveChangesAsync();


        }
    }
}

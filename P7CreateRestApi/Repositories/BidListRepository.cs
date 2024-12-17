using Dot.Net.WebApi.Controllers.Domain;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories
{
    public class BidListRepository : IRepository<BidList>
    {
        private readonly LocalDbContext _context;
        private readonly ILogger<BidListRepository> _logger;  


        public BidListRepository(LocalDbContext context, ILogger<BidListRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Add(BidList entity)
        {
            _context.BidLists.Add(entity);
            await _context.SaveChangesAsync();          
        }



        public async Task<IEnumerable<BidList>> GetAll()
        {
             return await  _context.BidLists.ToListAsync();         

        }

        public async Task<BidList> GetById(int id)
        {
            return await _context.BidLists.FirstOrDefaultAsync(r => r.BidListId == id);

        }

        public async Task Update(BidList entity)
        {
            var BidList = await _context.BidLists.FindAsync(entity.BidListId);          

            _context.Entry(BidList).State = EntityState.Detached;
            _context.BidLists.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {

            var BidList = await _context.BidLists.FindAsync(id);    
                _context.BidLists.Remove(BidList);
                await _context.SaveChangesAsync();


        }
    }
}

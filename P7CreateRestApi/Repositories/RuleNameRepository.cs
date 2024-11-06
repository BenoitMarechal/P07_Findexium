using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Controllers.Domain;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories
{
    public class RuleNameRepository : IRepository<RuleName>
    {
        private readonly LocalDbContext _context;
        private readonly ILogger<RuleNameRepository> _logger;


        public RuleNameRepository(LocalDbContext context, ILogger<RuleNameRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Add(RuleName entity)
        {
            _context.RuleNames.Add(entity);
            await _context.SaveChangesAsync();
        }



        public async Task<IEnumerable<RuleName>> GetAll()
        {
            return await _context.RuleNames.ToListAsync();

        }

        public async Task<RuleName> GetById(string id)
        {
            return await _context.RuleNames.FirstOrDefaultAsync(r => r.Id == id);

        }

        public async Task Update(RuleName entity)
        {
            var RuleName = await _context.RuleNames.FindAsync(entity.Id);

            _context.Entry(RuleName).State = EntityState.Detached;
            _context.RuleNames.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {

            var RuleName = await _context.RuleNames.FindAsync(id);
            _context.RuleNames.Remove(RuleName);
            await _context.SaveChangesAsync();


        }
    }
}

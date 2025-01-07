using Dot.Net.WebApi.Controllers.Domain;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories
{
    public class CurvePointRepository : IRepository<CurvePoint>
    {
        private readonly LocalDbContext _context;
        private readonly ILogger<CurvePointRepository> _logger;


        public CurvePointRepository(LocalDbContext context, ILogger<CurvePointRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Add(CurvePoint entity)
        {
            _context.CurvePoints.Add(entity);
            await _context.SaveChangesAsync();
        }



        public async Task<IEnumerable<CurvePoint>> GetAll()
        {
            return await _context.CurvePoints.ToListAsync();

        }

        public async Task<CurvePoint> GetById(int id)
        {
            return await _context.CurvePoints.FirstOrDefaultAsync(r => r.Id == id);

        }

        public async Task Update(CurvePoint entity)
        {
            var CurvePoint = await _context.CurvePoints.FindAsync(entity.Id);

            _context.Entry(CurvePoint).State = EntityState.Detached;
            _context.CurvePoints.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {

            var CurvePoint = await _context.CurvePoints.FindAsync(id);
            _context.CurvePoints.Remove(CurvePoint);
            await _context.SaveChangesAsync();


        }
    }
}

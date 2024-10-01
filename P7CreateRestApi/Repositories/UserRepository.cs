using Dot.Net.WebApi.Controllers.Domain;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly LocalDbContext _context;
        private readonly ILogger<UserRepository> _logger;


        public UserRepository(LocalDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Add(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
        }



        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();

        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(r => r.Id == id);

        }

        public async Task Update(User entity)
        {
            var User = await _context.Users.FindAsync(entity.Id);
            _context.Entry(User).State = EntityState.Detached;
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {

            var User = await _context.Users.FindAsync(id);
            _context.Users.Remove(User);
            await _context.SaveChangesAsync();


        }
    }
}

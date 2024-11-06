using Dot.Net.WebApi.Controllers.Domain;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories
{
    public class UserRepository : IRepository<IdentityUser>
    {
        private readonly LocalDbContext _context;
        private readonly ILogger<UserRepository> _logger;


        public UserRepository(LocalDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Add(IdentityUser entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
        }



        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            return await _context.Users.ToListAsync();

        }

        public async Task<IdentityUser> GetById(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(r => r.Id == id);

        }

        public async Task Update(IdentityUser entity)
        {
            var User = await _context.Users.FindAsync(entity.Id);
            _context.Entry(User).State = EntityState.Detached;
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {

            var User = await _context.Users.FindAsync(id);
            _context.Users.Remove(User);
            await _context.SaveChangesAsync();


        }

      

        //Task<IEnumerable<User>> IRepository<IdentityUser>.GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        public Task Add(User entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(User entity)
        {
            throw new NotImplementedException();
        }

    }
}

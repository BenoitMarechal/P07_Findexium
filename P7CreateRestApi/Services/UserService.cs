using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Repositories;
using Dot.Net.WebApi.Controllers.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace P7CreateRestApi.Services
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        // Adds a new User entity to the repository
        public async Task Add(User user)
        {
            await _userRepository.Add(user);
        }

        // Retrieves all User entities from the repository
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        // Retrieves a User by its ID, throws KeyNotFoundException if not found
        public async Task<User> GetById(int id)
        {
            var result = await _userRepository.GetById(id);
            if (result == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            return result;
        }

        // Updates a User entity, throws KeyNotFoundException if not found
        public async Task Update(User user)
        {
            var existingUser = await _userRepository.GetById(user.Id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {user.Id} not found.");
            }
            await _userRepository.Update(user);
        }

        // Deletes a User by its ID, throws KeyNotFoundException if not found
        public async Task Delete(int id)
        {
            var existingUser = await _userRepository.GetById(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            await _userRepository.Delete(id);
        }

        // Checks if a User exists in the repository by ID
        public async Task<bool> UserExists(int id)
        {
            return await _userRepository.GetById(id) != null;
        }
    }
}

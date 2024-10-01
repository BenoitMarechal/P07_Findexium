using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Repositories;
using Dot.Net.WebApi.Controllers.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace P7CreateRestApi.Services
{
    public class BidListService
    {
        private readonly IRepository<BidList> _bidListRepository;

        public BidListService(IRepository<BidList> bidListRepository)
        {
            _bidListRepository = bidListRepository;
        }

        // Adds a new BidList entity to the repository
        public async Task Add(BidList bidList)
        {
            await _bidListRepository.Add(bidList);
        }

        // Retrieves all BidList entities from the repository
        public async Task<IEnumerable<BidList>> GetAll()
        {
            return await _bidListRepository.GetAll();
        }

        // Retrieves a BidList by its ID, throws KeyNotFoundException if not found
        public async Task<BidList> GetById(int id)
        {
            var result = await _bidListRepository.GetById(id);
            if (result == null)
            {
                throw new KeyNotFoundException($"BidList with ID {id} not found.");
            }
            return result;
        }

        // Updates a BidList entity, throws KeyNotFoundException if not found
        public async Task Update(BidList bidList)
        {
            var existingBidList = await _bidListRepository.GetById(bidList.BidListId);
            if (existingBidList == null)
            {
                throw new KeyNotFoundException($"BidList with ID {bidList.BidListId} not found.");
            }
            await _bidListRepository.Update(bidList);
        }

        // Deletes a BidList by its ID, throws KeyNotFoundException if not found
        public async Task Delete(int id)
        {
            var existingBidList = await _bidListRepository.GetById(id);
            if (existingBidList == null)
            {
                throw new KeyNotFoundException($"BidList with ID {id} not found.");
            }
            await _bidListRepository.Delete(id);
        }

        // Checks if a BidList exists in the repository by ID
        public async Task<bool> BidListExists(int id)
        {
            return await _bidListRepository.GetById(id) != null;
        }
    }
}

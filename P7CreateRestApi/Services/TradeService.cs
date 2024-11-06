using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Repositories;
using Dot.Net.WebApi.Controllers.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace P7CreateRestApi.Services
{
    public class TradeService
    {
        private readonly IRepository<Trade> _tradeRepository;

        public TradeService(IRepository<Trade> tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        // Adds a new Trade entity to the repository
        public async Task Add(Trade trade)
        {
            await _tradeRepository.Add(trade);
        }

        // Retrieves all Trade entities from the repository
        public async Task<IEnumerable<Trade>> GetAll()
        {
            return await _tradeRepository.GetAll();
        }

        // Retrieves a Trade by its ID, throws KeyNotFoundException if not found
        public async Task<Trade> GetById(string id)
        {
            var result = await _tradeRepository.GetById(id);
            if (result == null)
            {
                throw new KeyNotFoundException($"Trade with ID {id} not found.");
            }
            return result;
        }

        // Updates a Trade entity, throws KeyNotFoundException if not found
        public async Task Update(Trade trade)
        {
            if (!await TradeExists(trade.TradeId))
            {
                throw new KeyNotFoundException($"Trade with ID {trade.TradeId} not found.");
            }
            await _tradeRepository.Update(trade);
        }

        // Deletes a Trade by its ID, throws KeyNotFoundException if not found
        public async Task Delete(string id)
        {
        
            if(!await TradeExists(id)) {
                throw new KeyNotFoundException($"Trade with ID {id} not found.");
            }



            await _tradeRepository.Delete(id);
        }

        // Checks if a Trade exists in the repository by ID
        public async Task<bool> TradeExists(string id)
        {
            return await _tradeRepository.GetById(id) != null;
        }
    }
}

using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Repositories;
using Dot.Net.WebApi.Controllers.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dot.Net.WebApi.Controllers;

namespace P7CreateRestApi.Services
{
    public class RuleNameService
    {
        private readonly IRepository<RuleName> _ruleNameRepository;

        public RuleNameService(IRepository<RuleName> ruleNameRepository)
        {
            _ruleNameRepository = ruleNameRepository;
        }

        // Adds a new RuleName entity to the repository
        public async Task Add(RuleName ruleName)
        {
            await _ruleNameRepository.Add(ruleName);
        }

        // Retrieves all RuleName entities from the repository
        public async Task<IEnumerable<RuleName>> GetAll()
        {
            return await _ruleNameRepository.GetAll();
        }

        // Retrieves a RuleName by its ID, throws KeyNotFoundException if not found
        public async Task<RuleName> GetById(int id)
        {
            var result = await _ruleNameRepository.GetById(id);
            if (result == null)
            {
                throw new KeyNotFoundException($"RuleName with ID {id} not found.");
            }
            return result;
        }

        // Updates a RuleName entity, throws KeyNotFoundException if not found
        public async Task Update(RuleName ruleName)
        {
            var existingRuleName = await _ruleNameRepository.GetById(ruleName.Id);
            if (existingRuleName == null)
            {
                throw new KeyNotFoundException($"RuleName with ID {ruleName.Id} not found.");
            }
            await _ruleNameRepository.Update(ruleName);
        }

        // Deletes a RuleName by its ID, throws KeyNotFoundException if not found
        public async Task Delete(int id)
        {
            var existingRuleName = await _ruleNameRepository.GetById(id);
            if (existingRuleName == null)
            {
                throw new KeyNotFoundException($"RuleName with ID {id} not found.");
            }
            await _ruleNameRepository.Delete(id);
        }

        // Checks if a RuleName exists in the repository by ID
        public async Task<bool> RuleNameExists(int id)
        {
            return await _ruleNameRepository.GetById(id) != null;
        }
    }
}

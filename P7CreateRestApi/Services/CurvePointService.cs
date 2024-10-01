using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Repositories;
using Dot.Net.WebApi.Controllers.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace P7CreateRestApi.Services
{
    public class CurvePointService
    {
        private readonly IRepository<CurvePoint> _curvePointRepository;

        public CurvePointService(IRepository<CurvePoint> curvePointRepository)
        {
            _curvePointRepository = curvePointRepository;
        }

        // Adds a new CurvePoint entity to the repository
        public async Task Add(CurvePoint curvePoint)
        {
            await _curvePointRepository.Add(curvePoint);
        }

        // Retrieves all CurvePoint entities from the repository
        public async Task<IEnumerable<CurvePoint>> GetAll()
        {
            return await _curvePointRepository.GetAll();
        }

        // Retrieves a CurvePoint by its ID, throws KeyNotFoundException if not found
        public async Task<CurvePoint> GetById(int id)
        {
            var result = await _curvePointRepository.GetById(id);
            if (result == null)
            {
                throw new KeyNotFoundException($"CurvePoint with ID {id} not found.");
            }
            return result;
        }

        // Updates a CurvePoint entity, throws KeyNotFoundException if not found
        public async Task Update(CurvePoint curvePoint)
        {
            var existingCurvePoint = await _curvePointRepository.GetById(curvePoint.Id);
            if (existingCurvePoint == null)
            {
                throw new KeyNotFoundException($"CurvePoint with ID {curvePoint.Id} not found.");
            }
            await _curvePointRepository.Update(curvePoint);
        }

        // Deletes a CurvePoint by its ID, throws KeyNotFoundException if not found
        public async Task Delete(int id)
        {
            var existingCurvePoint = await _curvePointRepository.GetById(id);
            if (existingCurvePoint == null)
            {
                throw new KeyNotFoundException($"CurvePoint with ID {id} not found.");
            }
            await _curvePointRepository.Delete(id);
        }

        // Checks if a CurvePoint exists in the repository by ID
        public async Task<bool> CurvePointExists(int id)
        {
            return await _curvePointRepository.GetById(id) != null;
        }
    }
}

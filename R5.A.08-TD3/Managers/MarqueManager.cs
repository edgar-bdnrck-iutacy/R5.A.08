using AutoMapper;
using R5.A._08_TD3.Models;
using R5.A._08_TD3.Models.DTO;
using R5.A._08_TD3.Repositories;

namespace R5.A._08_TD3.Managers
{
    public class MarqueManager : IMarqueManager
    {
        private readonly IMarqueRepository _repository;
        private readonly IMapper _mapper;

        public MarqueManager(
            IMarqueRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MarqueDto>> GetAllAsync()
        {
            var marques =
                await _repository.GetAllWithProductsAsync();

            return _mapper.Map<IEnumerable<MarqueDto>>(marques);
        }

        public async Task<MarqueDto?> GetByIdAsync(int id)
        {
            var marque =
                await _repository.GetByIdWithProductsAsync(id);

            if (marque == null)
            {
                return null;
            }

            return _mapper.Map<MarqueDto>(marque);
        }

        public async Task<Marque> AddAsync(Marque marque)
        {
            return await _repository.AddAsync(marque);
        }

        public async Task UpdateAsync(Marque marque)
        {
            await _repository.UpdateAsync(marque);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public bool Exists(int id)
        {
            return _repository.Exists(id);
        }
    }
}
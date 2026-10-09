using AutoMapper;
using R5.A._08_TD3.Models;
using R5.A._08_TD3.Models.DTO;
using R5.A._08_TD3.Repositories;

namespace R5.A._08_TD3.Managers
{
    public class TypeProduitManager : ITypeProduitManager
    {
        private readonly ITypeProduitRepository _repository;
        private readonly IMapper _mapper;

        public TypeProduitManager(
            ITypeProduitRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TypeProduitDto>> GetAllAsync()
        {
            var types = await _repository.GetAllWithProductsAsync();

            return _mapper.Map<IEnumerable<TypeProduitDto>>(types);
        }

        public async Task<TypeProduitDto?> GetByIdAsync(int id)
        {
            var typeProduit =
                await _repository.GetByIdWithProductsAsync(id);

            if (typeProduit == null)
            {
                return null;
            }

            return _mapper.Map<TypeProduitDto>(typeProduit);
        }

        public async Task<TypeProduit> AddAsync(TypeProduit typeProduit)
        {
            return await _repository.AddAsync(typeProduit);
        }

        public async Task UpdateAsync(TypeProduit typeProduit)
        {
            await _repository.UpdateAsync(typeProduit);
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
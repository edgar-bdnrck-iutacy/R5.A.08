using AutoMapper;
using R5.A._08_TD3.Profiles;
using R5.A._08_TD3.Models;
using R5.A._08_TD3.Models.DTO;
using R5.A._08_TD3.Models.Repository.Interfaces;

namespace R5.A._08_TD3.Managers
{
    public class ProduitManager : IProduitManager
    {
        private readonly IProduitRepository _repository;
        private readonly IMapper _mapper;

        public ProduitManager(IProduitRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProduitDto>> GetAllAsync()
        {
            var produits = await _repository.GetAllWithDetailsAsync();

            return _mapper.Map<IEnumerable<ProduitDto>>(produits);
        }

        public async Task<ProduitDetailDto?> GetByIdAsync(int id)
        {
            var produit = await _repository.GetByIdWithDetailsAsync(id);

            if (produit == null)
            {
                return null;
            }

            return _mapper.Map<ProduitDetailDto>(produit);
        }

        public async Task<Produit?> GetEntityByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ProduitDto>> GetByBrandAsync(int brandId)
        {
            var produits = await _repository.GetByBrandAsync(brandId);

            return _mapper.Map<IEnumerable<ProduitDto>>(produits);
        }

        public async Task AddAsync(Produit produit)
        {
            await _repository.AddAsync(produit);
        }

        public async Task UpdateAsync(Produit produit)
        {
            await _repository.UpdateAsync(produit);
        }

        public async Task DeleteAsync(Produit produit)
        {
            await _repository.DeleteAsync(produit);
        }

        public async Task SaveAsync()
        {
            await _repository.SaveAsync();
        }
    }
}
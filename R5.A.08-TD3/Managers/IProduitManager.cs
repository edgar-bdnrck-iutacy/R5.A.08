using R5.A._08_TD3.Models;
using R5.A._08_TD3.Models.DTO;

namespace R5.A._08_TD3.Managers
{
    public interface IProduitManager
    {
        Task<IEnumerable<ProduitDto>> GetAllAsync();
        Task<ProduitDetailDto?> GetByIdAsync(int id);
        Task<Produit?> GetEntityByIdAsync(int id);
        Task<IEnumerable<ProduitDto>> GetByBrandAsync(int brandId);
        Task AddAsync(Produit produit);
        Task UpdateAsync(Produit produit);
        Task DeleteAsync(Produit produit);
        Task SaveAsync();
    }
}
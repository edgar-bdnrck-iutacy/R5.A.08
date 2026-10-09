using R5.A._08_TD3.Models;
using R5.A._08_TD3.Models.DTO;

namespace R5.A._08_TD3.Managers
{
    public interface ITypeProduitManager
    {
        Task<IEnumerable<TypeProduitDto>> GetAllAsync();
        Task<TypeProduitDto?> GetByIdAsync(int id);

        Task<TypeProduit> AddAsync(TypeProduit typeProduit);
        Task UpdateAsync(TypeProduit typeProduit);
        Task DeleteAsync(int id);

        bool Exists(int id);
    }
}
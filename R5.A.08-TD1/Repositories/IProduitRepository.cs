using R5.A._08_TD1.Models;

namespace R5.A._08_TD1.Repositories
{
    public interface IProduitRepository
    {
        Task<IEnumerable<Produit>> GetAllAsync();
        Task<Produit?> GetByIdAsync(int id);
        Task<Produit> AddAsync(Produit produit);
        Task UpdateAsync(Produit produit);
        Task DeleteAsync(int id);
        bool Exists(int id);
    }
}
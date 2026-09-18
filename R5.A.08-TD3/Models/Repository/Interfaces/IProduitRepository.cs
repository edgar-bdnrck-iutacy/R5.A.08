using R5.A._08_TD3.Models;

namespace R5.A._08_TD3.Models.Repository.Interfaces
{
    public interface IProduitRepository : IRepository<Produit>
    {
        Task<IEnumerable<Produit>> GetAllWithDetailsAsync();
        Task<Produit?> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<Produit>> GetByBrandAsync(int brandId);
        Task<IEnumerable<Produit>> SearchByNameAsync(string term);
    }
}

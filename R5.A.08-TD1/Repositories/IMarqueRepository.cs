using R5.A._08_TD1.Models;

namespace R5.A._08_TD1.Repositories
{
    public interface IMarqueRepository
    {
        Task<IEnumerable<Marque>> GetAllAsync();
        Task<Marque?> GetByIdAsync(int id);
        Task<Marque> AddAsync(Marque marque);
        Task UpdateAsync(Marque marque);
        Task DeleteAsync(int id);
        bool Exists(int id);
    }
}
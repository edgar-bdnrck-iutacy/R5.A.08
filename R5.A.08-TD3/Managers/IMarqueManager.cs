using R5.A._08_TD3.Models;
using R5.A._08_TD3.Models.DTO;

namespace R5.A._08_TD3.Managers
{
    public interface IMarqueManager
    {
        Task<IEnumerable<MarqueDto>> GetAllAsync();
        Task<MarqueDto?> GetByIdAsync(int id);
        Task<Marque> AddAsync(Marque marque);
        Task UpdateAsync(Marque marque);
        Task DeleteAsync(int id);
        bool Exists(int id);
    }
}
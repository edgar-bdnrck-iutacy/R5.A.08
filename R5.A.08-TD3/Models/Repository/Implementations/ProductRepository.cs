using Microsoft.EntityFrameworkCore;
using R5.A._08_TD3.Models;
using R5.A._08_TD3.Models.Repository.Interfaces;

namespace R5.A._08_TD3.Models.Repository.Implementations
{
    public class ProductRepository : IProduitRepository
    {
        private readonly R508Context _context;

        public ProductRepository(R508Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produit>> GetAllAsync()
        {
            return await _context.Produits.ToListAsync();
        }

        public async Task<Produit?> GetByIdAsync(int id)
        {
            return await _context.Produits.FindAsync(id);
        }

        public async Task AddAsync(Produit entity)
        {
            await _context.Produits.AddAsync(entity);
        }

        public Task UpdateAsync(Produit entity)
        {
            _context.Produits.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Produit entity)
        {
            _context.Produits.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Produit>> GetAllWithDetailsAsync()
        {
            return await _context.Produits
                .Include(p => p.Marque)
                .Include(p => p.TypeProduit)
                .ToListAsync();
        }

        public async Task<Produit?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Produits
                .Include(p => p.Marque)
                .Include(p => p.TypeProduit)
                .FirstOrDefaultAsync(p => p.IdProduit == id);
        }

        public async Task<IEnumerable<Produit>> GetByBrandAsync(int brandId)
        {
            return await _context.Produits
                .Where(p => p.IdMarque == brandId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Produit>> SearchByNameAsync(string term)
        {
            return await _context.Produits
                .Where(p => p.NomProduit.Contains(term))
                .ToListAsync();
        }
    }
}

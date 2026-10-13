
using R5.A._08_TD4_Blazor.Models;

namespace R5.A._08_TD4_Blazor.Services
{
    public interface IService
    {
        // Récupérer tous les produits
        Task<List<ProduitDto>> GetProduitsAsync(
            CancellationToken cancellationToken = default);

        // Récupérer un produit par son identifiant
        Task<Produit?> GetProduitAsync(int id);

        // Créer un produit
        Task<Produit?> CreateProduitAsync(Produit produit);

        // Modifier un produit
        Task<bool> UpdateProduitAsync(int id, Produit produit);

        // Récupérer les marques
        Task<List<MarqueDto>> GetMarquesAsync();

        // Récupérer les types de produits
        Task<List<TypeProduitDto>> GetTypesProduitsAsync();
    }
}

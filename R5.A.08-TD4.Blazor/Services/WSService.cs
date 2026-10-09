
using System.Net;
using System.Net.Http.Json;
using R5.A._08_TD4_Blazor.Models;

namespace R5.A._08_TD4_Blazor.Services
{
    public class WSService : IService
    {
        private readonly HttpClient _httpClient;

        public WSService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProduitDto>> GetProduitsAsync()
        {
            return await _httpClient
                .GetFromJsonAsync<List<ProduitDto>>("api/Produits")
                ?? new List<ProduitDto>();
        }

        public async Task<Produit?> GetProduitAsync(int id)
        {
            using var response = await _httpClient.GetAsync(
                $"api/Produits/{id}/edit");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<Produit>();
        }

        public async Task<Produit?> CreateProduitAsync(Produit produit)
        {
            using var response = await _httpClient.PostAsJsonAsync(
                "api/Produits", produit);

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<Produit>();
        }

        public async Task<bool> UpdateProduitAsync(
            int id, Produit produit)
        {
            using var response = await _httpClient.PutAsJsonAsync(
                $"api/Produits/{id}", produit);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return false;

            response.EnsureSuccessStatusCode();

            return true;
        }

        public async Task<List<MarqueDto>> GetMarquesAsync()
        {
            return await _httpClient
                .GetFromJsonAsync<List<MarqueDto>>("api/Marques")
                ?? new List<MarqueDto>();
        }

        public async Task<List<TypeProduitDto>> GetTypesProduitsAsync()
        {
            return await _httpClient
                .GetFromJsonAsync<List<TypeProduitDto>>("api/TypeProduits")
                ?? new List<TypeProduitDto>();
        }

    }
}

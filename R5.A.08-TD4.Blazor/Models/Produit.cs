
using System.ComponentModel.DataAnnotations;

namespace R5.A._08_TD4_Blazor.Models
{
    public class Produit
    {
        public int IdProduit { get; set; }

        [Required(ErrorMessage = "Le nom du produit est obligatoire.")]
        [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères.")]
        public string NomProduit { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? NomPhoto { get; set; }

        public string? UriPhoto { get; set; }

        [Range(1, int.MaxValue,
            ErrorMessage = "Veuillez sélectionner un type de produit.")]
        public int IdTypeProduit { get; set; }

        [Range(1, int.MaxValue,
            ErrorMessage = "Veuillez sélectionner une marque.")]
        public int IdMarque { get; set; }

        [Range(0, int.MaxValue,
            ErrorMessage = "Le stock réel ne peut pas être négatif.")]
        public int StockReel { get; set; }

        [Range(0, int.MaxValue,
            ErrorMessage = "Le stock minimum ne peut pas être négatif.")]
        public int StockMin { get; set; }

        [Range(0, int.MaxValue,
            ErrorMessage = "Le stock maximum ne peut pas être négatif.")]
        public int StockMax { get; set; }
    }
}

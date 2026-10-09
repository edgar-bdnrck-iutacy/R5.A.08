
namespace R5.A._08_TD4_Blazor.Models
{
    public class Produit
    {
        public int IdProduit { get; set; }

        public string NomProduit { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? NomPhoto { get; set; }

        public string? UriPhoto { get; set; }

        public int IdTypeProduit { get; set; }

        public int IdMarque { get; set; }

        public int StockReel { get; set; }

        public int StockMin { get; set; }

        public int StockMax { get; set; }
    }
}

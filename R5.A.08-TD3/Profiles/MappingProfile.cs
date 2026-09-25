using AutoMapper;
using R5.A._08_TD3.Models;
using R5.A._08_TD3.Models.DTO;

namespace R5.A._08_TD3.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Produit -> ProduitDto
            CreateMap<Produit, ProduitDto>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.IdProduit))
                .ForMember(dest => dest.Nom,
                    opt => opt.MapFrom(src => src.NomProduit))
                .ForMember(dest => dest.Type,
                    opt => opt.MapFrom(src =>
                        src.TypeProduit != null
                            ? src.TypeProduit.NomTypeProduit
                            : null))
                .ForMember(dest => dest.Marque,
                    opt => opt.MapFrom(src =>
                        src.Marque != null
                            ? src.Marque.NomMarque
                            : null));

            // Produit -> ProduitDetailDto
            CreateMap<Produit, ProduitDetailDto>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.IdProduit))
                .ForMember(dest => dest.Nom,
                    opt => opt.MapFrom(src => src.NomProduit))
                .ForMember(dest => dest.Type,
                    opt => opt.MapFrom(src =>
                        src.TypeProduit != null
                            ? src.TypeProduit.NomTypeProduit
                            : null))
                .ForMember(dest => dest.Marque,
                    opt => opt.MapFrom(src =>
                        src.Marque != null
                            ? src.Marque.NomMarque
                            : null))
                .ForMember(dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Nomphoto,
                    opt => opt.MapFrom(src => src.NomPhoto))
                .ForMember(dest => dest.Uriphoto,
                    opt => opt.MapFrom(src => src.UriPhoto))
                .ForMember(dest => dest.Stock,
                    opt => opt.MapFrom(src => src.StockReel))
                .ForMember(dest => dest.EnReappro,
                    opt => opt.MapFrom(src =>
                        src.StockReel < src.StockMin));

            // Marque -> MarqueDto
            CreateMap<Marque, MarqueDto>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.IdMarque))
                .ForMember(dest => dest.Nom,
                    opt => opt.MapFrom(src => src.NomMarque))
                .ForMember(dest => dest.NbProduits,
                    opt => opt.MapFrom(src =>
                        src.Produits != null
                            ? src.Produits.Count
                            : 0));
        }
    }
}
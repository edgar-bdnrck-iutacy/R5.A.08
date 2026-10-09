
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using R5.A._08_TD4_Blazor.Models;
using R5.A._08_TD4_Blazor.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace R5.A._08_TD4_Blazor.ViewModels
{
    public partial class ProduitFormViewModel : ObservableObject
    {
        private readonly IService _service;

        public ProduitFormViewModel(IService service)
        {
            _service = service;
        }

        [ObservableProperty]
        private Produit produit = new();

        [ObservableProperty]
        private List<MarqueDto> marques = new();

        [ObservableProperty]
        private List<TypeProduitDto> typesProduits = new();

        [ObservableProperty]
        private string? erreur;

        [ObservableProperty]
        private string? succes;

        [ObservableProperty]
        private bool chargement;

        [RelayCommand]
        private async Task ChargerReferencesAsync()
        {
            Chargement = true;
            Erreur = null;

            try
            {
                Marques = await _service.GetMarquesAsync();
                TypesProduits = await _service.GetTypesProduitsAsync();
            }
            catch (Exception ex)
            {
                Erreur = "Erreur de chargement : " + ex.Message;
            }
            finally
            {
                Chargement = false;
            }
        }

        [RelayCommand]
        private async Task ChargerProduitAsync(int id)
        {
            Chargement = true;
            Erreur = null;

            try
            {
                var resultat = await _service.GetProduitAsync(id);

                if (resultat == null)
                {
                    Erreur = "Produit introuvable.";
                }
                else
                {
                    Produit = resultat;
                }
            }
            catch (Exception ex)
            {
                Erreur = "Impossible de charger le produit : "
                         + ex.Message;
            }
            finally
            {
                Chargement = false;
            }
        }

        [RelayCommand]
        private async Task CreerAsync()
        {
            Erreur = null;
            Succes = null;

            try
            {
                var resultat = await _service.CreateProduitAsync(Produit);

                if (resultat != null)
                {
                    Succes = "Produit créé avec succès.";
                    Produit = new Produit();
                }
                else
                {
                    Erreur = "La création du produit a échoué.";
                }
            }
            catch (Exception ex)
            {
                Erreur = "Erreur de création : " + ex.Message;
            }
        }

        [RelayCommand]
        private async Task ModifierAsync()
        {
            Erreur = null;
            Succes = null;

            try
            {
                var reussi = await _service.UpdateProduitAsync(
                    Produit.IdProduit, Produit);

                if (reussi)
                {
                    Succes = "Produit modifié avec succès.";
                }
                else
                {
                    Erreur = "Produit introuvable.";
                }
            }
            catch (Exception ex)
            {
                Erreur = "Erreur de modification : " + ex.Message;
            }
        }
    }
}

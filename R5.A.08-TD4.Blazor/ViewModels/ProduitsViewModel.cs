
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using R5.A._08_TD4_Blazor.Models;
using R5.A._08_TD4_Blazor.Services;

namespace R5.A._08_TD4_Blazor.ViewModels
{
    public partial class ProduitsViewModel : ObservableObject
    {
        private readonly IService _service;

        public ProduitsViewModel(IService service)
        {
            _service = service;
        }

        [ObservableProperty]
        private List<ProduitDto> produits = new();

        [ObservableProperty]
        private string? erreur;

        [RelayCommand(IncludeCancelCommand = true)]
        private async Task ChargerProduitsAsync(
            CancellationToken cancellationToken)
        {
            Erreur = null;

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var resultat = await _service.GetProduitsAsync();

                cancellationToken.ThrowIfCancellationRequested();

                Produits = resultat;
            }
            catch (OperationCanceledException)
            {
                // L'annulation est volontaire.
            }
            catch (Exception ex)
            {
                Erreur = "Impossible de charger les produits : "
                         + ex.Message;
            }
        }
    }
}


using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using R5.A._08_TD4.Blazor;
using R5.A._08_TD4_Blazor.Services;
using BlazorBootstrap;
using R5.A._08_TD4_Blazor.ViewModels;

namespace R5.A._08_TD4_Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Client HTTP configuré pour notre API TD3
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7219/")
            });

            // Injection du service de communication avec l'API
            builder.Services.AddScoped<IService, WSService>();

            builder.Services.AddBlazorBootstrap();

            builder.Services.AddTransient<ProduitsViewModel>();
            builder.Services.AddTransient<ProduitFormViewModel>();

            await builder.Build().RunAsync();
        }
    }
}

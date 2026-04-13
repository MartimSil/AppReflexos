using REFTEST.Pages.ING.Main;
using REFTEST.Pages.ING.Testes;
using System.Text.Json;

namespace REFTEST.Pages.ING.NotaTestes
{
    public partial class TesteEscolherNota : ContentPage
    {
        public TesteEscolherNota()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CarregarEOrdenarLista();
        }

        private void CarregarEOrdenarLista()
        {
            // Key kept as "historico_escolha" to sync with PT version
            string json = Preferences.Default.Get("historico_escolha", "[]");
            List<RegistoEscolher> lista = JsonSerializer.Deserialize<List<RegistoEscolher>>(json) ?? [];

            // Sort by points (highest to lowest)
            listaNotas.ItemsSource = lista.OrderByDescending(r => r.Pontos).ToList();
        }

        private async void OnApagarRegistroClicked(object sender, EventArgs e)
        {
            if (sender is HorizontalStackLayout layout &&
                layout.GestureRecognizers[0] is TapGestureRecognizer tap &&
                tap.CommandParameter is RegistoEscolher registro)
            {
                // Translated Alert
                bool confirmar = await DisplayAlertAsync("Delete", $"Delete result for {registro.Nome}?", "Yes", "No");

                if (confirmar)
                {
                    string json = Preferences.Default.Get("historico_escolha", "[]");
                    List<RegistoEscolher> lista = JsonSerializer.Deserialize<List<RegistoEscolher>>(json) ?? [];

                    var item = lista.FirstOrDefault(x => x.Nome == registro.Nome && x.Data == registro.Data);
                    if (item != null)
                    {
                        lista.Remove(item);
                        Preferences.Default.Set("historico_escolha", JsonSerializer.Serialize(lista));
                        CarregarEOrdenarLista();
                    }
                }
            }
        }

        private async void OnGoToHomePageNotasClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePageNotas());
        }
    }
}
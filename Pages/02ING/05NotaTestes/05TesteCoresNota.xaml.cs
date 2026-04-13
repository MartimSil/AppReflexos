using REFTEST.Pages.ING.Testes;
using System.Text.Json;

namespace REFTEST.Pages.ING.NotaTestes
{
    public partial class TesteCoresNota : ContentPage
    {
        public TesteCoresNota()
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
            // Mantemos a mesma chave "historico_cores" para que os dados sejam os mesmos
            string json = Preferences.Default.Get("historico_cores", "[]");
            List<RegistoCores> lista = JsonSerializer.Deserialize<List<RegistoCores>>(json) ?? [];

            // Sort by highest number of rules reached
            listaNotas.ItemsSource = lista.OrderByDescending(r => r.Regras).ToList();
        }

        private async void OnApagarRegistroClicked(object sender, EventArgs e)
        {
            if (sender is HorizontalStackLayout layout &&
                layout.GestureRecognizers[0] is TapGestureRecognizer tap &&
                tap.CommandParameter is RegistoCores registro)
            {
                // Alerta traduzido para Inglês
                bool confirmar = await DisplayAlertAsync("Delete", $"Delete result for {registro.Nome}?", "Yes", "No");

                if (confirmar)
                {
                    string json = Preferences.Default.Get("historico_cores", "[]");
                    List<RegistoCores> lista = JsonSerializer.Deserialize<List<RegistoCores>>(json) ?? [];

                    var item = lista.FirstOrDefault(x => x.Nome == registro.Nome && x.Data == registro.Data);
                    if (item != null)
                    {
                        lista.Remove(item);
                        Preferences.Default.Set("historico_cores", JsonSerializer.Serialize(lista));
                        CarregarEOrdenarLista();
                    }
                }
            }
        }

        private async void OnGoToHomePageNotasClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
using REFTEST.Pages.ING.Main;
using REFTEST.Pages.ING.Testes;
using System.Text.Json;

namespace REFTEST.Pages.ING.NotaTestes
{
    public partial class TesteVisaoNota : ContentPage
    {
        public TesteVisaoNota()
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
            string jsonGuardado = Preferences.Default.Get("historico_visao", "[]");
            var listaCompleta = JsonSerializer.Deserialize<List<RegistoVisao>>(jsonGuardado) ?? [];

            var listaOrdenada = listaCompleta
                .OrderByDescending(r => r.Pontos)
                .ToList();

            listaNotas.ItemsSource = listaOrdenada;
        }

        private async void OnApagarRegistroClicked(object sender, EventArgs e)
        {
            if (sender is HorizontalStackLayout layout && layout.GestureRecognizers[0] is TapGestureRecognizer recognizer && recognizer.CommandParameter is RegistoVisao registroParaApagar)
            {
                // Texto traduzido para Inglês
                bool confirmar = await DisplayAlertAsync("Delete", $"Do you want to delete the result for {registroParaApagar.Nome}?", "Yes", "No");

                if (confirmar)
                {
                    string jsonGuardado = Preferences.Default.Get("historico_visao", "[]");
                    var listaCompleta = JsonSerializer.Deserialize<List<RegistoVisao>>(jsonGuardado) ?? [];

                    var itemEncontrado = listaCompleta.FirstOrDefault(x => x.Nome == registroParaApagar.Nome && x.Data == registroParaApagar.Data);

                    if (itemEncontrado != null)
                    {
                        listaCompleta.Remove(itemEncontrado);

                        string jsonNovo = JsonSerializer.Serialize(listaCompleta);
                        Preferences.Default.Set("historico_visao", jsonNovo);

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
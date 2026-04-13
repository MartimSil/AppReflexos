using REFTEST.Pages.PT.Main;
using REFTEST.Pages.PT.Testes;
using System.Text.Json;

namespace REFTEST.Pages.PT.NotaTestes
{
    public partial class TesteContarNota : ContentPage
    {
        public TesteContarNota()
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
            string jsonGuardado = Preferences.Default.Get("historico_salvo", "[]");
            var listaCompleta = JsonSerializer.Deserialize<List<RegistoTempo>>(jsonGuardado) ?? [];

            var listaOrdenada = listaCompleta
                .OrderBy(r => Math.Abs(r.TempoAtingido - r.Alvo))
                .ToList();

            listaNotas.ItemsSource = listaOrdenada;
        }

        private async void OnApagarRegistroClicked(object sender, EventArgs e)
        {
            // O sender agora é o HorizontalStackLayout que recebeu o toque
            if (sender is HorizontalStackLayout layout && layout.GestureRecognizers[0] is TapGestureRecognizer recognizer && recognizer.CommandParameter is RegistoTempo registroParaApagar)
            {
                bool confirmar = await DisplayAlertAsync("Apagar", $"Queres apagar o resultado de {registroParaApagar.Nome}?", "Sim", "Não");

                if (confirmar)
                {
                    string jsonGuardado = Preferences.Default.Get("historico_salvo", "[]");
                    var listaCompleta = JsonSerializer.Deserialize<List<RegistoTempo>>(jsonGuardado) ?? [];

                    var itemEncontrado = listaCompleta.FirstOrDefault(x => x.Nome == registroParaApagar.Nome && x.Data == registroParaApagar.Data);

                    if (itemEncontrado != null)
                    {
                        listaCompleta.Remove(itemEncontrado);

                        string jsonNovo = JsonSerializer.Serialize(listaCompleta);
                        Preferences.Default.Set("historico_salvo", jsonNovo);

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
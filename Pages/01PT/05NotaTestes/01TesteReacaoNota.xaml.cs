using REFTEST.Pages.PT.Testes;
using System.Text.Json;

namespace REFTEST.Pages.PT.NotaTestes
{
    public partial class TesteReacaoNota : ContentPage
    {
        public TesteReacaoNota()
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
            string jsonGuardado = Preferences.Default.Get("historico_reacao", "[]");
            var listaCompleta = JsonSerializer.Deserialize<List<RegistoReacao>>(jsonGuardado) ?? [];

            var listaOrdenada = listaCompleta
                .OrderBy(r => r.Milissegundos)
                .ToList();

            listaNotas.ItemsSource = listaOrdenada;
        }

        private async void OnApagarRegistroClicked(object sender, EventArgs e)
        {
            if (sender is HorizontalStackLayout layout && layout.GestureRecognizers[0] is TapGestureRecognizer recognizer && recognizer.CommandParameter is RegistoReacao registroParaApagar)
            {
                bool confirmar = await DisplayAlertAsync("Apagar", $"Queres apagar o resultado de {registroParaApagar.Nome}?", "Sim", "Não");

                if (confirmar)
                {
                    string jsonGuardado = Preferences.Default.Get("historico_reacao", "[]");
                    var listaCompleta = JsonSerializer.Deserialize<List<RegistoReacao>>(jsonGuardado) ?? [];

                    var itemEncontrado = listaCompleta.FirstOrDefault(x => x.Nome == registroParaApagar.Nome && x.Data == registroParaApagar.Data);

                    if (itemEncontrado != null)
                    {
                        listaCompleta.Remove(itemEncontrado);

                        string jsonNovo = JsonSerializer.Serialize(listaCompleta);
                        Preferences.Default.Set("historico_reacao", jsonNovo);

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
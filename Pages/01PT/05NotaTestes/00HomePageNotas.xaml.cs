using REFTEST.Pages.PT.Main;

namespace REFTEST.Pages.PT.NotaTestes
{
    public partial class HomePageNotas : ContentPage
    {
        public HomePageNotas()
        {
            InitializeComponent();
        }
        //Pagina Principal
        private async void OnMainPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        //Testes
        private async void OnTesteReacaoNotaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteReacaoNota());
        }

        private async void OnTesteVisaoNotaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteVisaoNota());
        }

        private async void OnTesteContarNotaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteContarNota());
        }

        private async void OnTesteEscolherNotaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteEscolherNota());
        }

        private async void OnTesteCoresNotaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteCoresNota());
        }
    }
}
using REFTEST.Pages.ING.Testes;
//using REFTEST.Pages.ING.TesteMaster;
using REFTEST.Pages.ING.NotaTestes;

namespace REFTEST.Pages.ING.Main
{
    public partial class HomePageING : ContentPage
    {
        public HomePageING()
        {
            InitializeComponent();
        }
        //Pagina Principal
        private async void OnMainPageINGClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPageING());
        }

        //Testes
        private async void OnTesteReacaoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteReacao());
        }

        private async void OnTesteVisaoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteVisao());
        }

        private async void OnTesteContarClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteContar());
        }

        private async void OnTesteEscolherClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteEscolher());
        }

        private async void OnTesteCoresClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteCores());
        }
        //Teste Master
        /*private async void OnTesteReacaoMasterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteReacaoMaster());
        }*/
        private async void OnHomePageNotasClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePageNotas());
        }

    }
}
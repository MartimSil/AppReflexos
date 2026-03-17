using REFTEST.Pages.PT.Testes;
using REFTEST.Pages.PT.TesteMaster;

namespace REFTEST.Pages.PT.Main
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }
        //Pagina Principal
        private async void OnMainPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
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
        private async void OnTesteReacaoMasterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteReacaoMaster());
        }

    }
}
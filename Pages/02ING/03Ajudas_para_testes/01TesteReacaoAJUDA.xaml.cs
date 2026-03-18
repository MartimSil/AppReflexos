using REFTEST.Pages.ING.Main;
using REFTEST.Pages.ING.Testes;


namespace REFTEST.Pages.ING.Ajudas_para_testes
{
    public partial class TesteReacaoAJUDA : ContentPage
    {
        public TesteReacaoAJUDA()
        {
            InitializeComponent();
        }
        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePageING());
        }
        private async void OnTesteReacaoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteReacao());
        }
    }
}
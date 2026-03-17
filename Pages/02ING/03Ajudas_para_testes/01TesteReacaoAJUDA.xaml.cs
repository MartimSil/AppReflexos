using REFTEST.Pages.ING.Testes;
using REFTEST.Pages.ING.Main;

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
            await Navigation.PushAsync(new HomePage());
        }
        private async void OnTesteReacaoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteReacao());
        }
    }
}
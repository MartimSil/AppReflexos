using REFTEST.Pages.ING.Main;
using REFTEST.Pages.ING.Ajudas_para_testes;
using REFTEST.Pages.ING.NotaTestes;

namespace REFTEST.Pages.ING.Testes
{
    public partial class TesteReacao : ContentPage
    {
        public TesteReacao()
        {
            InitializeComponent();
        }
        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePageING());
        }
        private async void OnHelpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteReacaoAJUDA());
        }
        private async void OnGoToTesteReacaoNotaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteReacaoNota());
        }
    }
}
using REFTEST.Pages.ING.Ajudas_para_testes;
using REFTEST.Pages.ING.Main;
using REFTEST.Pages.ING.NotaTestes;

namespace REFTEST.Pages.ING.Testes
{
    public partial class TesteEscolher : ContentPage
    {
        public TesteEscolher()
        {
            InitializeComponent();
        }
        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
        private async void OnHelpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteEscolherAJUDA());
        }
        private async void OnGoToTesteEscolherNotaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteEscolherNota());
        }
    }
}
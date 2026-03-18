using REFTEST.Pages.ING.Main;
using REFTEST.Pages.ING.Ajudas_para_testes;
using REFTEST.Pages.ING.NotaTestes;

namespace REFTEST.Pages.ING.Testes
{
    public partial class TesteVisao : ContentPage
    {
        public TesteVisao()
        {
            InitializeComponent();
        }
        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePageING());
        }
        private async void OnHelpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteVisaoAJUDA());
        }
        private async void OnGoToTesteVisaoNotaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteVisaoNota());
        }
    }
}
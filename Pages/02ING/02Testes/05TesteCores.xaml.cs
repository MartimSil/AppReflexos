using REFTEST.Pages.ING.Main;
using REFTEST.Pages.ING.Ajudas_para_testes;
using REFTEST.Pages.ING.NotaTestes;

namespace REFTEST.Pages.ING.Testes
{
    public partial class TesteCores : ContentPage
    {
        public TesteCores()
        {
            InitializeComponent();
        }
        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePageING());
        }
        private async void OnHelpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteCoresAJUDA());
        }
        private async void OnGoToTestecoresNotaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteCoresNota());
        }
    }
}


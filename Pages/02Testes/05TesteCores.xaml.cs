using REFTEST.Pages.Ajudas_para_testes;

namespace REFTEST.Pages.Testes
{
    public partial class TesteCores : ContentPage
    {
        public TesteCores()
        {
            InitializeComponent();
        }
        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
        private async void OnHelpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteCoresAJUDA());
        }
    }
}


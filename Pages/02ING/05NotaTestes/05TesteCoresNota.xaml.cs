using REFTEST.Pages.ING.Main;
using REFTEST.Pages.ING.Testes;
using REFTEST.Pages.ING.Ajudas_para_testes;

namespace REFTEST.Pages.ING.NotaTestes
{
    public partial class TesteCoresNota : ContentPage
    {
        public TesteCoresNota()
        {
            InitializeComponent();
        }
        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
    }
}
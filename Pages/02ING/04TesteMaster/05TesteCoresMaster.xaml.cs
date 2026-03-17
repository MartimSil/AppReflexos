using REFTEST.Pages.ING.Ajudas_para_testes;

namespace REFTEST.Pages.ING.TesteMaster
{
    public partial class TesteCoresMaster : ContentPage
    {
        public TesteCoresMaster()
        {
            InitializeComponent();
        }
        private async void OnGoToNotaMasterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotaMaster());
        }
        private async void OnHelpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteCoresAJUDA());
        }
    }
}


using REFTEST.Pages.PT.Ajudas_para_testes;

namespace REFTEST.Pages.PT.TesteMaster
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


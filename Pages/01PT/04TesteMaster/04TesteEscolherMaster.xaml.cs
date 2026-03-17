using REFTEST.Pages.PT.Ajudas_para_testes;

namespace REFTEST.Pages.PT.TesteMaster
{
    public partial class TesteEscolherMaster : ContentPage
    {
        public TesteEscolherMaster()
        {
            InitializeComponent();
        }
        private async void OnGoToTesteCoresMasterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteCoresMaster());
        }
        private async void OnHelpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteEscolherAJUDA());
        }
    }
}
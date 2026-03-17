using REFTEST.Pages.ING.Ajudas_para_testes;

namespace REFTEST.Pages.ING.TesteMaster
{
    public partial class TesteVisaoMaster : ContentPage
    {
        public TesteVisaoMaster()
        {
            InitializeComponent();
        }
        private async void OnGoToTesteContarMasterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteContarMaster());
        }
        private async void OnHelpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteVisaoAJUDA());
        }
    }
}
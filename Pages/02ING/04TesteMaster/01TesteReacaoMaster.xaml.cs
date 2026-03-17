using REFTEST.Pages.ING.Ajudas_para_testes;

namespace REFTEST.Pages.ING.TesteMaster
{
    public partial class TesteReacaoMaster : ContentPage
    {
        public TesteReacaoMaster()
        {
            InitializeComponent();
        }
        private async void OnGoToTesteVisaoMasterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteVisaoMaster());
        }
        private async void OnHelpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteReacaoAJUDA());
        }
    }
}
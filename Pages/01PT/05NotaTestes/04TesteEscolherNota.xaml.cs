using REFTEST.Pages.PT.Main;
using REFTEST.Pages.PT.Testes;
using REFTEST.Pages.PT.Ajudas_para_testes;

namespace REFTEST.Pages.PT.NotaTestes
{
    public partial class TesteEscolherNota : ContentPage
    {
        public TesteEscolherNota()
        {
            InitializeComponent();
        }
        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
    }
}
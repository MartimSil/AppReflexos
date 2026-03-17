using REFTEST.Pages.PT.Main;

namespace REFTEST.Pages.PT.NotaTestes
{
    public partial class TesteReacaoNota : ContentPage
    {
        public TesteReacaoNota()
        {
            InitializeComponent();
        }
        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
    }
}
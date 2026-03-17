using REFTEST.Pages.ING.Main;

namespace REFTEST.Pages.ING.NotaTestes
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
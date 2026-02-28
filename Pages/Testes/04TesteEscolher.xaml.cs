namespace REFTEST.Pages.Testes
{
    public partial class TesteEscolher : ContentPage
    {
        public TesteEscolher()
        {
            InitializeComponent();
        }
        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
    }
}
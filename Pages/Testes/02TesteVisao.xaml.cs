namespace REFTEST.Pages.Testes
{
    public partial class TesteVisao : ContentPage
    {
        public TesteVisao()
        {
            InitializeComponent();
        }
        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
    }
}

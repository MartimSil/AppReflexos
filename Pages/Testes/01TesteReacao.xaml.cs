namespace REFTEST.Pages.Testes
{
    public partial class TesteReacao : ContentPage
    {
        public TesteReacao()
        {
            InitializeComponent();
        }
        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
    }
}
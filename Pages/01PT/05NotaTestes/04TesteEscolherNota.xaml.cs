namespace REFTEST.Pages.PT.NotaTestes
{
    public partial class TesteEscolherNota : ContentPage
    {
        public TesteEscolherNota()
        {
            InitializeComponent();
        }
        private async void OnGoToHomePageNotasClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePageNotas());
        }
    }
}
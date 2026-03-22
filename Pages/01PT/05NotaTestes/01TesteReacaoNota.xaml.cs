namespace REFTEST.Pages.PT.NotaTestes
{
    public partial class TesteReacaoNota : ContentPage
    {
        public TesteReacaoNota()
        {
            InitializeComponent();
        }
        private async void OnGoToHomePageNotasClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePageNotas());
        }
    }
}
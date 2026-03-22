namespace REFTEST.Pages.PT.NotaTestes
{
    public partial class TesteVisaoNota : ContentPage
    {
        public TesteVisaoNota()
        {
            InitializeComponent();
        }
        private async void OnGoToHomePageNotasClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePageNotas());
        }
    }
}
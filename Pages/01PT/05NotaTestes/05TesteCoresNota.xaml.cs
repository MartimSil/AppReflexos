namespace REFTEST.Pages.PT.NotaTestes
{
    public partial class TesteCoresNota : ContentPage
    {
        public TesteCoresNota()
        {
            InitializeComponent();
        }
        private async void OnGoToHomePageNotasClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePageNotas());
        }
    }
}
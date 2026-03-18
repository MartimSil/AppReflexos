using REFTEST.Pages.ING.Main;

namespace REFTEST.Pages.ING.NotaTestes
{
    public partial class TesteCoresNota : ContentPage
    {
        public TesteCoresNota()
        {
            InitializeComponent();
        }
        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePageING());
        }
    }
}
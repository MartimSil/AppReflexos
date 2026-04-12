using REFTEST.Pages.DEF.Main;

namespace REFTEST.Pages.ING.Main
{
    public partial class MainPageING : ContentPage
    {
        public MainPageING()
        {
            InitializeComponent();
        }

        private async void OnGoToHomePageINGClicked(object sender, EventArgs e) => await Navigation.PushAsync(new HomePageING());

        private async void OnGoToDefINGClicked(object sender, EventArgs e) => await Navigation.PushAsync(new DefING());

    }
}
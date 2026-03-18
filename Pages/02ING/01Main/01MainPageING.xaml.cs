using REFTEST.Pages.DEF;

namespace REFTEST.Pages.ING.Main
{
    public partial class MainPageING : ContentPage
    {
        public MainPageING()
        {
            InitializeComponent();
        }

        private async void OnGoToHomePageINGClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePageING());
        }
        private async void OnGoToLanguagePageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LanguagePage());
        }
    }
}
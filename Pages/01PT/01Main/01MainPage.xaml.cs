using REFTEST.Pages.DEF;

namespace REFTEST.Pages.PT.Main
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
        private async void OnGoToDEFClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LanguagePage());
        }
    }
}
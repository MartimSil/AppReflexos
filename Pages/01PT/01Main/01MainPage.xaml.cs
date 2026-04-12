using REFTEST.Pages.DEF.Main;

namespace REFTEST.Pages.PT.Main
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnGoToHomeClicked(object sender, EventArgs e) => await Navigation.PushAsync(new HomePage());

        private async void OnGoToDefPTClicked(object sender, EventArgs e) => await Navigation.PushAsync(new DefPT());
    }
}
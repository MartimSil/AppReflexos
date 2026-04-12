using REFTEST.Pages.PT.Main;
using REFTEST.Pages.DEF.Languages;

namespace REFTEST.Pages.DEF.Main
{
    public partial class DefPT : ContentPage
    {
        public DefPT()
        {
            InitializeComponent();
        }
        private async void OnGoToPORTClicked(object sender, EventArgs e) => await Navigation.PushAsync(new PORT());
        private async void OnGoToMainPageClicked(object sender, EventArgs e) => await Navigation.PushAsync(new MainPage());
    }
}
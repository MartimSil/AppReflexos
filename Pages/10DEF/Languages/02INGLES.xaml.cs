using REFTEST.Pages.PT.Main;
using REFTEST.Pages.ING.Main;

namespace REFTEST.Pages.DEF.Languages
{
    public partial class INGLES : ContentPage
    {
        public INGLES()
        {
            InitializeComponent();
        }
        private async void OnMainPageClicked(object sender, EventArgs e) => await Navigation.PushAsync(new MainPage());
        private async void OnMainPageINGClicked(object sender, EventArgs e) => await Navigation.PushAsync(new MainPageING());
        private async void OnEnglishClicked(object sender, EventArgs e)
        {
            REFTEST.Helpers.Localization.SetCulture("en-US");
            await Navigation.PushAsync(new MainPageING());
        }

    }
}
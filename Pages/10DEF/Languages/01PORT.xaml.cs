using REFTEST.Pages.PT.Main;
using REFTEST.Pages.ING.Main;

namespace REFTEST.Pages.DEF.Languages
{
    public partial class PORT : ContentPage
    {
        public PORT()
        {
            InitializeComponent();
        }

        private async void OnGoToMainPageINGClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPageING());
        }
        private async void OnMainPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
        private async void OnPortugueseClicked(object sender, EventArgs e)
        {
            REFTEST.Helpers.Localization.SetCulture("pt-PT");
            await Navigation.PushAsync(new MainPage());
        }
}
}
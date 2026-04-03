using REFTEST.Pages.ING.Main;
using REFTEST.Pages.PT.Main;
namespace REFTEST.Pages.DEF
{
    public partial class LanguagePage : ContentPage
    {
        public LanguagePage()
        {
            InitializeComponent();
        }
        private async void OnMainPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
        private async void OnMainPageINGClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPageING());
        }
    }
}
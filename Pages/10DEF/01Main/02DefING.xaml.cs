using REFTEST.Pages.ING.Main;
using REFTEST.Pages.DEF.Languages;

namespace REFTEST.Pages.DEF.Main
{
    public partial class DefING : ContentPage
    {
        public DefING()
        {
            InitializeComponent();
        }
        private async void OnGoToINGLESClicked(object sender, EventArgs e) => await Navigation.PushAsync(new INGLES()); 
        private async void OnMainPageINGClicked(object sender, EventArgs e) => await Navigation.PushAsync(new MainPageING());
    }
}
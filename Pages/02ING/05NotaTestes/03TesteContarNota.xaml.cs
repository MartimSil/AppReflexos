using REFTEST.Pages.ING.Main;

namespace REFTEST.Pages.ING.NotaTestes
{
    public partial class TesteContarNota : ContentPage
    {
        public TesteContarNota()
        {
            InitializeComponent();
        }
        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePageING());
        }
    }
}
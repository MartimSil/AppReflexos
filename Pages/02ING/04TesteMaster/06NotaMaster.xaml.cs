using REFTEST.Pages.ING.Main;

namespace REFTEST.Pages.ING.TesteMaster
{
    public partial class NotaMaster : ContentPage
    {
        public NotaMaster()
        {
            InitializeComponent();
        }
        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
    }
}

